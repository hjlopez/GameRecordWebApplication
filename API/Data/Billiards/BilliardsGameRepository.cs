using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.Billiards;
using API.Interfaces.Billiards;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Billiards
{
    public class BilliardsGameRepository : IBilliardsGameRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public BilliardsGameRepository(DataContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public void DeleteMatch(BilliardsMatch billiardsMatch)
        {
            context.BilliardsMatches.Remove(billiardsMatch);
        }

        public async Task<IEnumerable<BilliardsMatch>> GetMatchesAsync()
        {
            return await context.BilliardsMatches.ToListAsync();
        }

        public async Task<IEnumerable<BilliardsMatch>> GetMatchesByModeAsync(int modeId)
        {
            return await context.BilliardsMatches.Where(x => x.ModeId == modeId).ToListAsync();
        }

        public async Task<IEnumerable<BilliardsMatch>> GetMatchesBySeasonAsync(int seasonNumberId)
        {
            return await context.BilliardsMatches.Where(x => x.SeasonNumberId == seasonNumberId).ToListAsync();
        }

        public async Task<IEnumerable<BilliardsMatch>> GetMatchesByTournamentAsync(int tournamentId)
        {
            return await context.BilliardsMatches.Where(x => x.TournamentId == tournamentId).ToListAsync();
        }

        public async Task<IEnumerable<BilliardsMatch>> GetMatchesByTypeAsync(int typeId)
        {
            return await context.BilliardsMatches.Where(x => x.TypeId == typeId).ToListAsync();
        }

        public async Task<IEnumerable<BilliardsMatch>> GetMatchesByUserAsync(int userId)
        {
            return await context.BilliardsMatches.Where(x => x.WinUserId == userId || x.LoseUserId == userId).ToListAsync();
        }

        public async Task<BilliardsMatch> GetSingleMatchAsync(int id)
        {
            return await context.BilliardsMatches.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public void InsertMatch(BilliardsMatch billiardsMatch)
        {
            context.BilliardsMatches.Add(billiardsMatch);
        }

        public void UpdateMatch(BilliardsMatch billiardsMatch)
        {
            context.Entry(billiardsMatch).State = EntityState.Modified;
        }
    }
}