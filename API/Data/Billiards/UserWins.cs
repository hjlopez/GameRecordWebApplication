using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Billiards;
using API.Entities.Billiards;
using API.Interfaces.Billiards;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Billiards
{
    public class UserWins : IUserWins
    {
        private readonly IMapper mapper;
        private readonly DataContext context;

        public UserWins(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<UserWinsDto> GetTotalUserWinsVsUser(int userId, int opponentUserId)
        {
            var dto = new UserWinsDto();

            var query1 = await context.BilliardsMatches.Where((x => x.WinUserId == userId && x.LoseUserId == opponentUserId)).ToListAsync();

            var query2 = await context.BilliardsMatches.Where((y => y.LoseUserId == userId && y.WinUserId == opponentUserId)).ToListAsync();

            foreach (var item in query1)
            {
                dto.UserWins += item.WinnerWins;
            }

            foreach (var item in query2)
            {
                dto.UserWins += item.LoserWins;
            }

            dto.UserId = userId;
            return dto;

            
        }

        public async Task<UserWinsDto> GetTotalUserWinsVsUserSeason(int userId, int opponentUserId, int tournamentId, int seasonId)
        {
            var dto = new UserWinsDto();

            var query1 = await context.BilliardsMatches
                .Where((x => x.WinUserId == userId && x.LoseUserId == opponentUserId 
                        && x.TournamentId == tournamentId && x.SeasonNumberId == seasonId)).ToListAsync();

            var query2 = await context.BilliardsMatches
                .Where((y => y.LoseUserId == userId && y.WinUserId == opponentUserId 
                        && y.TournamentId == tournamentId && y.SeasonNumberId == seasonId)).ToListAsync();

            foreach (var item in query1)
            {
                dto.UserWins += item.WinnerWins;
            }

            foreach (var item in query2)
            {
                dto.UserWins += item.LoserWins;
            }

            dto.UserId = userId;
            return dto;
        }

        public async Task<UserWinsDto> GetTotalUserWinsVsUserTournament(int userId, int opponentUserId, int tournamentId)
        {
            var dto = new UserWinsDto();

            var query1 = await context.BilliardsMatches
                .Where((x => x.WinUserId == userId && x.LoseUserId == opponentUserId && x.TournamentId == tournamentId)).ToListAsync();

            var query2 = await context.BilliardsMatches
                .Where((y => y.LoseUserId == userId && y.WinUserId == opponentUserId && y.TournamentId == tournamentId)).ToListAsync();

            foreach (var item in query1)
            {
                dto.UserWins += item.WinnerWins;
            }

            foreach (var item in query2)
            {
                dto.UserWins += item.LoserWins;
            }

            dto.UserId = userId;
            return dto;
        }
        
        public async Task<UserWinsDto> GetTotalUserWinsVsUserType(int userId, int opponentUserId, int tournamentId, int seasonId, int typeId)
        {
            var dto = new UserWinsDto();

            var query1 = await context.BilliardsMatches
                .Where((x => x.WinUserId == userId && x.LoseUserId == opponentUserId 
                        && x.TournamentId == tournamentId && x.SeasonNumberId == seasonId && x.TypeId == typeId)).ToListAsync();

            var query2 = await context.BilliardsMatches
                .Where((y => y.LoseUserId == userId && y.WinUserId == opponentUserId 
                        && y.TournamentId == tournamentId && y.SeasonNumberId == seasonId && y.TypeId == typeId)).ToListAsync();

            foreach (var item in query1)
            {
                dto.UserWins += item.WinnerWins;
            }

            foreach (var item in query2)
            {
                dto.UserWins += item.LoserWins;
            }

            dto.UserId = userId;
            return dto;
        }

        public async Task<UserWinsDto> GetUserTotalWins(int userId)
        {
            var dto = new UserWinsDto();
            var query = await context.BilliardsMatches.Where(x => x.WinUserId == userId).ToListAsync();


            foreach (var item in query)
            {
                dto.UserWins += item.WinnerWins;
            }

            query = await context.BilliardsMatches.Where(x => x.LoseUserId == userId).ToListAsync();

            foreach (var item in query)
            {
                dto.UserWins += item.LoserWins;
            }

            dto.UserId = userId;

            return dto;
        }
    
        public async Task<IEnumerable<BilliardsMatchDto>> GetAllMatchesVsUser(int userId, int opponentUserId, int tournamentId)
        {
            var matches = await context.BilliardsMatches
                .Where(t => t.TournamentId == tournamentId
                        && ((t.WinUserId == userId && t.LoseUserId == opponentUserId)
                        || (t.WinUserId == opponentUserId && t.LoseUserId == userId)))
                .ToListAsync();
            
            return mapper.Map<BilliardsMatchDto[]>(matches);
        }

        public async Task<IEnumerable<BilliardsMatchDto>> GetSeasonMatchesVsUser(int userId, int opponentUserId, int tournamentId, 
                                                                                    int seasonNumberId)
        {
            var matches = await context.BilliardsMatches
                .Where(t => t.TournamentId == tournamentId && t.SeasonNumberId == seasonNumberId
                        && ((t.WinUserId == userId && t.LoseUserId == opponentUserId)
                        || (t.WinUserId == opponentUserId && t.LoseUserId == userId)))
                .ToListAsync();
            
            return mapper.Map<BilliardsMatchDto[]>(matches);
        }

        public async Task<IEnumerable<BilliardsMatchDto>> GetTypeMatchesVsUser(int userId, int opponentUserId, int tournamentId, 
                                                                                int typeId)
        {
            var matches = await context.BilliardsMatches
                .Where(t => t.TournamentId == tournamentId && t.TypeId == typeId
                        && ((t.WinUserId == userId && t.LoseUserId == opponentUserId)
                        || (t.WinUserId == opponentUserId && t.LoseUserId == userId)))
                .ToListAsync();
            
            return mapper.Map<BilliardsMatchDto[]>(matches);
        }

        public async Task<IEnumerable<BilliardsMatchDto>> GetNonPlayoffMatchesVsUser(int userId, int opponentUserId, int tournamentId)
        {
            var matches = await context.BilliardsMatches
                .Where(t => t.TournamentId == tournamentId
                        && ((t.WinUserId == userId && t.LoseUserId == opponentUserId)
                        || (t.WinUserId == opponentUserId && t.LoseUserId == userId)))
                .ToListAsync();
            return mapper.Map<BilliardsMatchDto[]>(matches);
        }

        public async Task<IEnumerable<BilliardsMatchDto>> GetPlayoffMatchesVsUser(int userId, int opponentUserId, int tournamentId)
        {
            var matches = await context.BilliardsMatches
                .Where(t => t.TournamentId == tournamentId
                        && ((t.WinUserId == userId && t.LoseUserId == opponentUserId)
                        || (t.WinUserId == opponentUserId && t.LoseUserId == userId)))
                .ToListAsync();
            return mapper.Map<BilliardsMatchDto[]>(matches);
        }

        public async Task<UserWinsDto> GetUserTotalWinsSeasonType(int userId, int seasonNumberId, int typeId)
        {
            var dto = new UserWinsDto();

            var query1 = await context.BilliardsMatches
                .Where((x => x.WinUserId == userId
                        && x.SeasonNumberId == seasonNumberId
                        && x.TypeId == typeId)).ToListAsync();

            var query2 = await context.BilliardsMatches
                .Where((y => y.LoseUserId == userId
                        && y.SeasonNumberId == seasonNumberId
                        && y.TypeId == typeId)).ToListAsync();

            foreach (var item in query1)
            {
                dto.UserWins += item.WinnerWins;
            }

            foreach (var item in query2)
            {
                dto.UserWins += item.LoserWins;
            }

            dto.UserId = userId;
            return dto;
        }
    }
}