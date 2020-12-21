using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IGamesRepository
    {
        Task<IEnumerable<GamesDto>> GetGames();
    }
}