using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs.Billiards;
using API.Entities.Billiards;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Billiards
{
    [Authorize]
    public class BilliardsGameController : BaseApiController
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public BilliardsGameController(IUnitOfWork unitOfWork, IMapper mapper)
        {
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

        [HttpGet("get-match-tournament/{tournamentId}")]
        public async Task<ActionResult<IEnumerable<BilliardsMatchDto>>> GetMatchesByTournament(int tournamentId)
        {
            var tournament = await unitOfWork.BilliardsTournamentRepository.GetTournamentById(tournamentId);
            if (tournament == null) return BadRequest("Invalid tournament.");

            var matches = await unitOfWork.BilliardsGameRepository.GetMatchesBySeasonAsync(tournament.Id);
            return Ok(mapper.Map<IEnumerable<BilliardsMatchDto>>(matches));
        }

        [HttpPost("insert-match")]
        public async Task<ActionResult> InsertMatch(BilliardsMatchDto billiardsMatchDto)
        {
            var userWin = await unitOfWork.UserRepository.GetUserByIdAsync(billiardsMatchDto.WinUserId);
            if (userWin == null) return BadRequest("Invalid user.");
            var userLose = await unitOfWork.UserRepository.GetUserByIdAsync(billiardsMatchDto.LoseUserId);
            if (userLose == null) return BadRequest("Invalid user.");
            var type = await unitOfWork.BilliardsMatchTypesRepository.GetMatchTypeByIdAsync(billiardsMatchDto.TypeId);
            if (type == null) return BadRequest("Invalid type.");
            var mode = await unitOfWork.BilliardsModeRepository.GetModeByIdAsync(billiardsMatchDto.ModeId);
            if (mode == null) return BadRequest("Invalid mode.");
            var season = await unitOfWork.BilliardsRepository.GetSeasonBySeasonNumberId(billiardsMatchDto.SeasonNumberId);
            if (season == null) return BadRequest("Invalid season.");
            var tournament = await unitOfWork.BilliardsTournamentRepository.GetTournamentById(billiardsMatchDto.TournamentId);
            if (tournament == null) return BadRequest("Invalid tournament.");

            unitOfWork.BilliardsGameRepository.InsertMatch(mapper.Map<BilliardsMatch>(billiardsMatchDto));
            if (await unitOfWork.Complete()) return NoContent();

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

            unitOfWork.BilliardsGameRepository.DeleteMatch(match);
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Delete failed.");
        }
    }
}