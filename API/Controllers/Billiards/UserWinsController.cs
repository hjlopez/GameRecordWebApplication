using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.Billiards;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Billiards
{
    public class UserWinsController : BaseApiController
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public UserWinsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet("get-season-type")]
        // public async Task<ActionResult<UserWinsDto>> GetSeasonTypeMatchUp(UserWinsDto userWinsDto)
        public async Task<ActionResult<UserWinsDto>> GetSeasonTypeMatchUp([FromQuery] int userId, int opponentUserId, int typeId,
                                                                                int seasonNumberId, int tournamentId)
        {      
            // return Ok();     
            return Ok(await unitOfWork.UserWins.GetTotalUserWinsVsUserType(userId, opponentUserId, tournamentId, seasonNumberId, typeId));
        }

        [HttpGet("get-season-matchup")]
        public async Task<ActionResult<UserWinsDto>> GetSeasonTypeMatchUp([FromQuery] int userId, int opponentUserId,
                                                                                int seasonNumberId, int tournamentId)
        {
            return Ok(await unitOfWork.UserWins.GetTotalUserWinsVsUserSeason(userId, opponentUserId, tournamentId, seasonNumberId));
        }

        [HttpPost("get-alltime-match")]
        public async Task<ActionResult<BilliardsTournamentMembersDto>> GetAllTimeMatch(BilliardsTournamentMembersDto dto)
        {
            var matches = await unitOfWork.UserWins.GetAllMatchesVsUser(dto.UserId, dto.OpponentUserId, dto.TournamentId);

            var newDto = new BilliardsTournamentMembersDto();
            
            // total all time wins
            foreach (var match in matches)
            {
                if (match.WinUserId == dto.UserId)
                {
                    newDto.Wins += match.WinnerWins;
                }
                else
                {
                    newDto.Wins += match.LoserWins;
                }

                newDto.TotalGamesPlayed += match.TotalGamesPlayed;
            }

            return Ok(newDto);
        }

        [HttpPost("get-season-match")]
        public async Task<ActionResult<BilliardsTournamentMembersDto>> GetSeasonMatch(BilliardsTournamentMembersDto dto)
        {
            var matches = await unitOfWork.UserWins.GetSeasonMatchesVsUser(dto.UserId, dto.OpponentUserId, dto.TournamentId, dto.SeasonNumberId);

            var newDto = new BilliardsTournamentMembersDto();
            
            // total all time wins
            foreach (var match in matches)
            {
                if (match.WinUserId == dto.UserId)
                {
                    newDto.Wins += match.WinnerWins;
                }
                else
                {
                    newDto.Wins += match.LoserWins;
                }

                newDto.TotalGamesPlayed += match.TotalGamesPlayed;
            }

            return Ok(newDto);
        }
    
        [HttpPost("get-type-match")]
        public async Task<ActionResult<BilliardsTournamentMembersDto>> GetTypeMatch(BilliardsTournamentMembersDto dto)
        {
            var matches = await unitOfWork.UserWins.GetTypeMatchesVsUser(dto.UserId, dto.OpponentUserId, dto.TournamentId, dto.TypeId);

            var newDto = new BilliardsTournamentMembersDto();
            
            // total all time wins
            foreach (var match in matches)
            {
                if (match.WinUserId == dto.UserId)
                {
                    newDto.Wins += match.WinnerWins;
                }
                else
                {
                    newDto.Wins += match.LoserWins;
                }

                newDto.TotalGamesPlayed += match.TotalGamesPlayed;
            }

            return Ok(newDto);
        }

        [HttpPost("get-nonplayoff-match")]
        public async Task<ActionResult<BilliardsTournamentMembersDto>> GetNonPlayoffMatch(BilliardsTournamentMembersDto dto)
        {
            var matches = await unitOfWork.UserWins.GetNonPlayoffMatchesVsUser(dto.UserId, dto.OpponentUserId, dto.TournamentId);
            var modes = await unitOfWork.BilliardsModeRepository.GetTournamentModesAsync(dto.TournamentId);


            var newDto = new BilliardsTournamentMembersDto();
            
            // total all time wins
            foreach (var match in matches)
            {
                foreach (var mode in modes)
                {
                    // count non playoff wins
                    if (!mode.IsPlayoff && mode.ModeId == match.ModeId)
                    {
                        if (match.WinUserId == dto.UserId)
                        {
                            newDto.Wins += match.WinnerWins;
                        }
                        else
                        {
                            newDto.Wins += match.LoserWins;
                        }

                        newDto.TotalGamesPlayed += match.TotalGamesPlayed;
                    }
                }
                
            }

            return Ok(newDto);
        }
    
        [HttpPost("get-playoff-match")]
        public async Task<ActionResult<BilliardsTournamentMembersDto>> GetPlayoffMatch(BilliardsTournamentMembersDto dto)
        {
            var matches = await unitOfWork.UserWins.GetPlayoffMatchesVsUser(dto.UserId, dto.OpponentUserId, dto.TournamentId);
            var modes = await unitOfWork.BilliardsModeRepository.GetTournamentModesAsync(dto.TournamentId);


            var newDto = new BilliardsTournamentMembersDto();
            
            // total all time wins
            foreach (var match in matches)
            {
                foreach (var mode in modes)
                {
                    // count non playoff wins
                    if (mode.IsPlayoff && mode.ModeId == match.ModeId)
                    {
                        if (match.WinUserId == dto.UserId)
                        {
                            newDto.Wins += match.WinnerWins;
                        }
                        else
                        {
                            newDto.Wins += match.LoserWins;
                        }

                        newDto.TotalGamesPlayed += match.TotalGamesPlayed;
                    }
                }
                
            }

            return Ok(newDto);
        }
    }
}