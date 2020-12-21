using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class BilliardsTournamentRepository : IBilliardsTournamentRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public BilliardsTournamentRepository(DataContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public void DeleteTournament(Tournament tournament)
        {
            context.Tournament.Remove(tournament);
        }

        public async Task<Tournament> GetTournamentById(int id)
        {
            return await context.Tournament.FindAsync(id);
        }

        public async Task<Tournament> GetTournamentDto(string tournamentName)
        {
            return await context.Tournament.FirstOrDefaultAsync(t => t.TournamentName == tournamentName);   
        }

        public async Task<IEnumerable<BilliardsTournamentDto>> GetTournamentList()
        {
            var tournaments = await context.Tournament.ToListAsync();

            return mapper.Map<IEnumerable<BilliardsTournamentDto>>(tournaments);
        }

        public void InsertTournament(BilliardsTournamentDto dto)
        {
            context.Tournament.AddAsync(mapper.Map<Tournament>(dto));
        }

        public void UpdateTournament(Tournament tournament)
        {
            context.Entry(tournament).State = EntityState.Modified;
        }
    }
}