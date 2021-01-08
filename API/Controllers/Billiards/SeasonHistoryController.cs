using System.Threading.Tasks;
using API.DTOs.Billiards;
using API.Entities;
using API.Entities.Billiards;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Billiards
{
    [Authorize]
    public class SeasonHistoryController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public SeasonHistoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost("insert-history")]
        public async Task<ActionResult> InsertSeasonHistory(SeasonHistoryDto seasonHistoryDto)
        {
            if (await unitOfWork.BilliardsTournamentRepository.GetTournamentById(seasonHistoryDto.TournamentId) == null)
                return BadRequest("Invalid tournament.");

            if (await unitOfWork.UserRepository.GetUserByIdAsync(seasonHistoryDto.UserId) == null)
                return BadRequest("Invalid user.");

            var type = await unitOfWork.BilliardsMatchTypesRepository.GetMatchTypeByIdAsync(seasonHistoryDto.TypeId);
            if (type == null) return BadRequest("Invalid type.");

            if (await unitOfWork.BilliardsRepository.GetSeasonBySeasonNumberId(seasonHistoryDto.SeasonNumberId) == null)
                return BadRequest("Invalid season.");
            
            var mode = await unitOfWork.BilliardsModeRepository.GetModeByIdAsync(seasonHistoryDto.ModeId);
            if (mode == null) return BadRequest("Invalid mode.");

            var history = mapper.Map<SeasonHistory>(seasonHistoryDto);
            unitOfWork.SeasonHistoryRepository.InsertSeasonHistory(history);
            
            if (await unitOfWork.Complete()) return Ok();

            return BadRequest("Error in season history insert.");
        }



        [HttpDelete("delete-history/{matchId}")]
        public async Task<ActionResult> DeleteSeasonHistory(int matchId)
        {
            var match = await unitOfWork.BilliardsGameRepository.GetSingleMatchAsync(matchId);
            if (match == null) return BadRequest("Match not found.");

            var history = await unitOfWork.SeasonHistoryRepository
                                .GetSeasonHistory(match.WinUserId, match.SeasonNumberId, match.TournamentId, match.TypeId);
            unitOfWork.SeasonHistoryRepository.DeleteSeasonHistory(history);
            if (!await unitOfWork.Complete()) return BadRequest("Cannot delete winner");

            history = await unitOfWork.SeasonHistoryRepository
                                .GetSeasonHistory(match.LoseUserId, match.SeasonNumberId, match.TournamentId, match.TypeId);
            unitOfWork.SeasonHistoryRepository.DeleteSeasonHistory(history);
            if (!await unitOfWork.Complete()) return BadRequest("Cannot delete loser");

            return Ok();
        }
    }
}