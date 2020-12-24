using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Billiards;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Billiards
{
    public class BilliardsMatchTypesRepository : IBilliardsMatchTypesRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public BilliardsMatchTypesRepository(DataContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public void DeleteMatchType(BilliardsMatchType type)
        {
            context.BilliardsMatchTypes.Remove(type);
        }

        public async Task<IEnumerable<BilliardsMatchType>> GetMatchTypesAsync()
        {
            return await context.BilliardsMatchTypes.ToListAsync();
        }

        public async Task<BilliardsMatchType> GetMatchTypeByIdAsync(int id)
        {
            return await context.BilliardsMatchTypes.Where(s => s.Id == id).SingleOrDefaultAsync();

        }

        public async Task<BilliardsMatchType> GetMatchTypeByNameAsync(string typeName)
        {
            return await context.BilliardsMatchTypes.Where(s => s.Type.ToLower() == typeName.ToLower()).SingleOrDefaultAsync();
        }

        public void InsertMatchType(BilliardsMatchType type)
        {
            context.BilliardsMatchTypes.Add(type);
        }

        public void UpdateMatchType(BilliardsMatchType type)
        {
            context.Entry(type).State = EntityState.Modified;
        }

        public async Task<IEnumerable<TournamentMatchTypesDto>> GetTournamentMatchTypes(int tournamentId)
        {
            var value = await context.TournamentMatchTypes.Where(x => x.TournamentId == tournamentId).ToListAsync();

            return mapper.Map<IEnumerable<TournamentMatchTypesDto>>(value);
        }

        public void InsertTournamentMatchTypes(TournamentMatchType tournamentMatchType)
        {
            context.TournamentMatchTypes.Add(tournamentMatchType);
        }

        public void RemoveTournamentMatchTypes(TournamentMatchType tournamentMatchType)
        {
            context.TournamentMatchTypes.Remove(tournamentMatchType);
        }

        public async Task<TournamentMatchTypesDto> GetTournamentMatchType(int matchId, int tournamentId)
        {
            var value = await context.TournamentMatchTypes
                .Where(x => x.TournamentId == tournamentId && x.MatchTypeId == matchId).SingleOrDefaultAsync();

            return mapper.Map<TournamentMatchTypesDto>(value);
        }

        public async Task<TournamentMatchType> GetTournamentMatchType(int id)
        {
            return await context.TournamentMatchTypes.Where(x => x.Id == id).SingleOrDefaultAsync();

        }

        public async Task<TournamentMatchType> GetTournamentMatchTypeByMatchIdAsync(int matchId)
        {
            return await context.TournamentMatchTypes.Where(x => x.MatchTypeId == matchId).SingleOrDefaultAsync();
        }
    }
}