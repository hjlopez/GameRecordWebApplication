using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.Billiards;
using API.Entities;
using API.Entities.Billiards;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Billiards
{
    [Authorize]
    public class BilliardsGameController : BaseApiController
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;
        public BilliardsGameController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("get-matches")]
        public async Task<ActionResult<IEnumerable<BilliardsMatchDto>>> GetMatches()
        {
            var matches = await unitOfWork.BilliardsGameRepository.GetMatchesAsync();
            return Ok(mapper.Map<IEnumerable<BilliardsMatchDto>>(matches));
        }

        [HttpGet("get-match/{id}")]
        public async Task<ActionResult<BilliardsMatchDto>> GetMatch(int id)
        {
            var match = await unitOfWork.BilliardsGameRepository.GetSingleMatchAsync(id);
            return Ok(mapper.Map<BilliardsMatchDto>(match));
        }

        [HttpGet("get-match-user/{userId}")]
        public async Task<ActionResult<IEnumerable<BilliardsMatchDto>>> GetMatchesByUser(int userId)
        {
            var user = await unitOfWork.UserRepository.GetUserByIdAsync(userId);
            if (user == null) return BadRequest("Invalid user.");

            var matches = await unitOfWork.BilliardsGameRepository.GetMatchesByUserAsync(user.Id);
            return Ok(mapper.Map<IEnumerable<BilliardsMatchDto>>(matches));
        }

        [HttpGet("get-match-type/{typeId}")]
        public async Task<ActionResult<IEnumerable<BilliardsMatchDto>>> GetMatchesByType(int typeId)
        {
            var type = await unitOfWork.BilliardsMatchTypesRepository.GetMatchTypeByIdAsync(typeId);
            if (type == null) return BadRequest("Invalid type.");

            var matches = await unitOfWork.BilliardsGameRepository.GetMatchesByTypeAsync(type.Id);
            return Ok(mapper.Map<IEnumerable<BilliardsMatchDto>>(matches));
        }

        [HttpGet("get-match-mode/{modeId}")]
        public async Task<ActionResult<IEnumerable<BilliardsMatchDto>>> GetMatchesByMode(int modeId)
        {
            var mode = await unitOfWork.BilliardsModeRepository.GetModeByIdAsync(modeId);
            if (mode == null) return BadRequest("Invalid mode.");

            var matches = await unitOfWork.BilliardsGameRepository.GetMatchesByModeAsync(mode.Id);
            return Ok(mapper.Map<IEnumerable<BilliardsMatchDto>>(matches));
        }

        [HttpGet("get-match-season/{seasonNumberId}")]
        public async Task<ActionResult<IEnumerable<BilliardsMatchDto>>> GetMatchesBySeason(int seasonNumberId)
        {
            var season = await unitOfWork.BilliardsRepository.GetSeasonBySeasonNumberId(seasonNumberId);
            if (season == null) return BadRequest("Invalid season.");

            var matches = await unitOfWork.BilliardsGameRepository.GetMatchesBySeasonAsync(season.Id);
            return Ok(mapper.Map<IEnumerable<BilliardsMatchDto>>(matches));
        }

        [AllowAnonymous]
        [HttpGet("get-match-tournament")]
        public async Task<ActionResult<IEnumerable<BilliardsMatchDto>>> GetMatchesByTournament([FromQuery] BilliardsMatchParams matchParams)
        {

            var tournament = await unitOfWork.BilliardsTournamentRepository.GetTournamentById(matchParams.TournamentId);
            if (tournament == null) return BadRequest("Invalid tournament.");

            var matches = await unitOfWork.BilliardsGameRepository.GetMatchesByTournamentAsync(matchParams);

            Response.AddPaginationHeader(matches.CurrentPage, matches.PageSize, matches.TotalCount, matches.TotalPages);

            return Ok(matches);
        }

        [AllowAnonymous]
        [HttpGet("get-filtered-match")]
        public async Task<ActionResult<IEnumerable<BilliardsMatchDto>>> GetFilteredMatches([FromQuery] BilliardsMatchParams matchParams)
        {
            var matches = await unitOfWork.BilliardsGameRepository.GetFilteredMatches(matchParams);

            Response.AddPaginationHeader(matches.CurrentPage, matches.PageSize, matches.TotalCount, matches.TotalPages);

            return Ok(matches);
        }

        private async Task<AppUser> CheckUser(int userId)
        {
            return await unitOfWork.UserRepository.GetUserByIdAsync(userId);

        }

        private async Task<BilliardsMatchType> CheckType(int typeId)
        {
            return await unitOfWork.BilliardsMatchTypesRepository.GetMatchTypeByIdAsync(typeId);
        }

        private async Task<BilliardsMode> CheckMode(int modeId)
        {
            return await unitOfWork.BilliardsModeRepository.GetModeByIdAsync(modeId);
        }

        private async Task<Season> CheckSeason(int seasonNumberId)
        {
            return await unitOfWork.BilliardsRepository.GetSeasonBySeasonNumberId(seasonNumberId);
        }

        private async Task<Tournament> CheckTournament(int tournamentId)
        {
            return await unitOfWork.BilliardsTournamentRepository.GetTournamentById(tournamentId);
        }

        [HttpPost("insert-match")]
        public async Task<ActionResult> InsertMatch(BilliardsMatchDto billiardsMatchDto)
        {

            var type = await unitOfWork.BilliardsMatchTypesRepository.GetMatchTypeByIdAsync(billiardsMatchDto.TypeId);
            if (type == null) return BadRequest("Invalid type.");

            var tournament = await unitOfWork.BilliardsTournamentRepository.GetTournamentById(billiardsMatchDto.TournamentId);
            if (tournament == null) return BadRequest("Invalid tournament.");

            // check if season is done by checking if all type for tournament and season has final mode
            // if (await unitOfWork.BilliardsGameRepository.CheckIfSeasonIsDone(billiardsMatchDto.SeasonNumberId))
            //     return BadRequest("This season is done. You cannot edit it anymore.");
            
            // before insert, check first if the final mode is already inserted for the type, season and tournament
            var lastMode = await unitOfWork.BilliardsModeRepository.GetTournamentLastModeAsync(tournament.Id);
            
            // check if last mode is already inserted to prevent new matches
            var history = await unitOfWork.BilliardsGameRepository.CheckIfLastModeIsPlayed(lastMode.ModeId, billiardsMatchDto.SeasonNumberId,
                    billiardsMatchDto.TournamentId, billiardsMatchDto.TypeId);
            if (history != null) return BadRequest("Cannot insert anymore to " + type.Type + " for this season");

            unitOfWork.BilliardsGameRepository.InsertMatch(mapper.Map<BilliardsMatch>(billiardsMatchDto));
            if (await unitOfWork.Complete())
            {
                return NoContent();
            } 

            return BadRequest("Insert failed.");
        }

        [HttpPut("update-match")]
        public async Task<ActionResult> UpdateMatch(BilliardsMatchDto billiardsMatchDto)
        {
            var match = await unitOfWork.BilliardsGameRepository.GetSingleMatchAsync(billiardsMatchDto.Id);
            if (match == null) return BadRequest("Match does not exist.");

            mapper.Map(billiardsMatchDto, match);
            unitOfWork.BilliardsGameRepository.UpdateMatch(match);
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Update failed.");
        }

        [HttpDelete("delete-match/{id}")]
        public async Task<ActionResult> DeleteMatch(int id)
        {
            var match = await unitOfWork.BilliardsGameRepository.GetSingleMatchAsync(id);
            if (match == null) return BadRequest("Match does not exist.");

            // before insert, check first if the final mode is already inserted for the type, season and tournament
            var lastMode = await unitOfWork.BilliardsModeRepository.GetTournamentLastModeAsync(match.TournamentId);
            
            // check if last mode is already inserted to prevent new matches
            if (match.ModeId != lastMode.ModeId)
            {
                var history = await unitOfWork.BilliardsGameRepository.CheckIfLastModeIsPlayed(lastMode.ModeId, match.SeasonNumberId,
                    match.TournamentId, match.TypeId);
                if (history != null) return BadRequest("Delete the last game for this type first.");
            }
            

            unitOfWork.BilliardsGameRepository.DeleteMatch(match);
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Delete failed.");
        }

        
    }
}