using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.Billiards;
using API.Entities.Billiards;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class BilliardsController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public BilliardsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("get-billiards-tournament/{userId}")]
        public async Task<ActionResult<IEnumerable<BilliardsTournamentDto>>> GetBilliardsTournaments(int userId)
        {
            var tournaments = await unitOfWork.BilliardsRepository.GetTournamentsForUserAsync(userId);

            foreach (var tour in tournaments)
            {
                var value = await unitOfWork.BilliardsTournamentRepository.GetTournamentById(tour.TournamentId);
                tour.TournamentName = value.TournamentName;
            }
            return Ok(tournaments);
        }

        [HttpGet("get-tournament-seasons/{tournamentId}")]
        public async Task<ActionResult<IEnumerable<SeasonDto>>> GetTournamentSeasons(int tournamentId)
        {
            var seasons = await unitOfWork.BilliardsRepository.GetSeasonsForTournamentsAsync(tournamentId);
            return Ok(mapper.Map<IEnumerable<SeasonDto>>(seasons));
        }

        [HttpPost("insert-tournament-season")]
        public async Task<ActionResult> InsertTournamentSeason(SeasonDto seasonDto)
        {
            var tour = await unitOfWork.BilliardsTournamentRepository.GetTournamentById(seasonDto.TournamentId);
            if (tour == null) return BadRequest("Tournamend invalid!");

            unitOfWork.BilliardsRepository.InsertSeasonForTournament(mapper.Map<Season>(seasonDto));
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Insert failed!");
        }

        [HttpDelete("delete-tournament-season/{id}")]
        public async Task<ActionResult> DeleteTournamentSeason(int id)
        {
            var cur = await unitOfWork.BilliardsRepository.GetSeasonForTournamentAsync(id);
            if (cur == null) return BadRequest("No such season!");
            if (cur.IsDone) return BadRequest("Cannot delete done seasons.");

            unitOfWork.BilliardsRepository.DeleteSeasonFromTournament(cur);
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Delete failed!");
        }

        [HttpPut("update-tournament-season")]
        public async Task<ActionResult> UpdateTournamentSeason(SeasonDto seasonDto)
        {
            var cur = await unitOfWork.BilliardsRepository.GetSeasonForTournamentAsync(seasonDto.Id);
            if (cur == null) return BadRequest("No such season!");

            // update to done, season cannot be updated or deleted anymore
            cur.IsDone = true;
            unitOfWork.BilliardsRepository.UpdateSeason(cur);
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Update failed!");
        }

        [HttpGet("get-history")]
        public async Task<ActionResult<IEnumerable<SeasonHistoryDto>>> GetSeasonHistory()
        {
            var hist = await unitOfWork.BilliardsRepository.GetHistoryAsync();
            return Ok(mapper.Map<IEnumerable<SeasonHistoryDto>>(hist));
        }

        [HttpGet("get-history-season/{seasonNumber}")]
        public async Task<ActionResult<IEnumerable<SeasonHistoryDto>>> GetSeasonHistoryBySeason(int seasonNumber)
        {
            var season = await unitOfWork.BilliardsRepository.GetSeasonBySeasonNumber(seasonNumber);
            if (season == null) return BadRequest("Invalid season.");

            var hist = await unitOfWork.BilliardsRepository.GetHistoryBySeasonAsync(season.SeasonNumber);
            return Ok(mapper.Map<IEnumerable<SeasonHistoryDto>>(hist));
        }

        [HttpGet("get-history-tournament/{tournamentId}")]
        public async Task<ActionResult<IEnumerable<SeasonHistoryDto>>> GetSeasonHistoryByTournament(int tournamentId)
        {
            var tournament = await unitOfWork.BilliardsTournamentRepository.GetTournamentById(tournamentId);
            if (tournament == null) return BadRequest("Invalid tournament.");

            var hist = await unitOfWork.BilliardsRepository.GetHistoryByTournamentAsync(tournament.Id);
            return Ok(mapper.Map<IEnumerable<SeasonHistoryDto>>(hist));
        }

        [HttpGet("get-history-type/{typeId}")]
        public async Task<ActionResult<IEnumerable<SeasonHistoryDto>>> GetSeasonHistoryByType(int typeId)
        {
            var type = await unitOfWork.BilliardsMatchTypesRepository.GetMatchTypeByIdAsync(typeId);
            if (type == null) return BadRequest("Invalid type.");

            var hist = await unitOfWork.BilliardsRepository.GetHistoryByTypeAsync(type.Id);
            return Ok(mapper.Map<IEnumerable<SeasonHistoryDto>>(hist));
        }

        [HttpGet("get-history-user/{userId}")]
        public async Task<ActionResult<IEnumerable<SeasonHistoryDto>>> GetSeasonHistoryByUser(int userId)
        {
            var user = await unitOfWork.UserRepository.GetUserByIdAsync(userId);
            if (user == null) return BadRequest("Invalid user.");

            var hist = await unitOfWork.BilliardsRepository.GetHistoryByUserAsync(user.Id);
            return Ok(mapper.Map<IEnumerable<SeasonHistoryDto>>(hist));
        }

        [HttpPost("insert-history")]
        public async Task<ActionResult> InserSeasonHistory(SeasonHistoryDto seasonHistoryDto)
        {
            var season = await unitOfWork.BilliardsRepository.GetSeasonBySeasonNumber(seasonHistoryDto.SeasonNumber);
            if (season == null) return BadRequest("Invalid season.");

            var tournament = await unitOfWork.BilliardsTournamentRepository.GetTournamentById(seasonHistoryDto.TournamentId);
            if (tournament == null) return BadRequest("Invalid tournament.");

            var type = await unitOfWork.BilliardsMatchTypesRepository.GetMatchTypeByIdAsync(seasonHistoryDto.TypeId);
            if (type == null) return BadRequest("Invalid type.");

            var user = await unitOfWork.UserRepository.GetUserByIdAsync(seasonHistoryDto.UserId);
            if (user == null) return BadRequest("Invalid user.");

            unitOfWork.BilliardsRepository.InsertSeasonHistory(mapper.Map<SeasonHistory>(seasonHistoryDto));
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Insert failed.");
        }

        [HttpDelete("delete-history-type/{typeId}")]
        public async Task<ActionResult> DeleteHistoryByType(int typeId)
        {
            var type = await unitOfWork.BilliardsMatchTypesRepository.GetMatchTypeByIdAsync(typeId);
            if (type == null) return BadRequest("Invalid type.");

            var hist = await unitOfWork.BilliardsRepository.GetHistoryByTypeAsync(type.Id);
            foreach (var history in hist)
            {
                unitOfWork.BilliardsRepository.DeleteSeasonHistory(history);
            }
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Delete failed.");
        }
    }
}