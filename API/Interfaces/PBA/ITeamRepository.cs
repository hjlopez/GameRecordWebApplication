using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities.PBA;

namespace API.Interfaces.PBA
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetTeamsAsync();
        Task<Team> GetTeamAsync(int id);
        void UpdateTeam(Team team);
    }
}