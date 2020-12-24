using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs.Billiards;
using API.Entities;

namespace API.Interfaces
{
    public interface IBilliardsMatchTypesRepository
    {
        Task<IEnumerable<BilliardsMatchType>> GetMatchTypesAsync();
        Task<BilliardsMatchType> GetMatchTypeByIdAsync(int id);
        Task<BilliardsMatchType> GetMatchTypeByNameAsync(string typeName);
        Task<IEnumerable<TournamentMatchTypesDto>> GetTournamentMatchTypes(int tournamentId);
        Task<TournamentMatchTypesDto> GetTournamentMatchType(int matchId, int tournamentId);
        Task<TournamentMatchType> GetTournamentMatchType(int id);
        Task<TournamentMatchType> GetTournamentMatchTypeByMatchIdAsync(int matchId);
        void InsertTournamentMatchTypes(TournamentMatchType tournamentMatchType);
        void RemoveTournamentMatchTypes(TournamentMatchType tournamentMatchType);
        void InsertMatchType(BilliardsMatchType type);
        void UpdateMatchType(BilliardsMatchType type);
        void DeleteMatchType(BilliardsMatchType type);
    }
}