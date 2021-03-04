using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.PBA;
using API.Interfaces.PBA;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.PBA
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public TeamRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Team> GetTeamAsync(int id)
        {
            return await context.Teams.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Team>> GetTeamsAsync()
        {
            return await context.Teams.ToListAsync();
        }

        public void UpdateTeam(Team team)
        {
            context.Entry(team).State = EntityState.Modified;
        }
    }
}