using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Billiards;
using API.Entities.Billiards;
using API.Helpers;
using API.Interfaces.Billiards;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public async Task<PagedList<BilliardsMatchDto>> GetFilteredMatches(BilliardsMatchParams matchParams)
        {
            var query = context.BilliardsMatches.AsQueryable();

            query = query.Where(x => x.TournamentId == matchParams.TournamentId); 

            if (matchParams.TypeId != 0) query = query.Where(x => x.TypeId == matchParams.TypeId); 
            if (matchParams.ModeId != 0) query = query.Where(x => x.ModeId == matchParams.ModeId);
            if (matchParams.SeasonNumberId != 0) query = query.Where(x => x.SeasonNumberId == matchParams.SeasonNumberId); 

            query = query.OrderByDescending(s => s.Id).ThenByDescending(s => s.SeasonNumberId);

            return await PagedList<BilliardsMatchDto>.CreateAsync(query.ProjectTo<BilliardsMatchDto>(mapper.ConfigurationProvider), 
                matchParams.PageNumber, matchParams.PageSize);
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

        public async Task<PagedList<BilliardsMatchDto>> GetMatchesByTournamentAsync(BilliardsMatchParams matchParams)
        {
            var query = context.BilliardsMatches.AsQueryable();

            query = query.Where(x => x.TournamentId == matchParams.TournamentId); 

            query = query.OrderByDescending(s => s.Id).ThenByDescending(s => s.SeasonNumberId);

            return await PagedList<BilliardsMatchDto>.CreateAsync(query.ProjectTo<BilliardsMatchDto>(mapper.ConfigurationProvider), 
                matchParams.PageNumber, matchParams.PageSize);
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