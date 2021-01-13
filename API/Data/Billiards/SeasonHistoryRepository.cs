using System.Collections;
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
    public class SeasonHistoryRepository : ISeasonHistoryRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public SeasonHistoryRepository(DataContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public void DeleteSeasonHistory(SeasonHistory seasonHistory)
        {
            context.SeasonHistories.Remove(seasonHistory);
        }

        public Task<SeasonHistoryDto> GetSeasonHistory()
        {
            throw new System.NotImplementedException();
        }

        public async Task<SeasonHistory> GetSeasonHistory(int userId, int seasonNumberId, int tournamentId, int typeId)
        {
            return await context.SeasonHistories.Where(x => x.UserId == userId && x.SeasonNumberId == seasonNumberId &&
                                                            x.TournamentId == tournamentId && x.TypeId == typeId).SingleOrDefaultAsync();
        }

        public Task<SeasonHistoryDto> GetSeasonHistoryBySeason(int seasonNumberId)
        {
            throw new System.NotImplementedException();
        }

        public Task<SeasonHistoryDto> GetSeasonHistoryByTournament(int tournamentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<SeasonHistoryDto> GetSeasonHistoryByType(int typeId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<SeasonHistory>> GetSeasonHistoryForChecking(int modeId, int seasonNumberId, int tournamentId, int typeId
                                                                                , int userId)
        {
            return await context.SeasonHistories.Where(x => x.ModeId == modeId && x.SeasonNumberId == seasonNumberId &&
                                                            x.TournamentId == tournamentId && x.TypeId == typeId &&
                                                            x.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<SeasonHistoryDto>> GetSeasonRank(int tournamentId, int seasonNumberId, int typeId)
        {
            var history = await context.SeasonHistories
                .Where(x => x.TournamentId == tournamentId && x.SeasonNumberId == seasonNumberId && x.TypeId == typeId)
                .OrderBy(r => r.Rank)
                .ToListAsync();

            return mapper.Map<IEnumerable<SeasonHistoryDto>>(history);
        }

        public void InsertSeasonHistory(SeasonHistory seasonHistory)
        {
            context.SeasonHistories.Add(seasonHistory);
        }
    }
}