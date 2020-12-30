using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities.Billiards;

namespace API.Interfaces.Billiards
{
    public interface IBilliardsGameRepository
    {
        Task<IEnumerable<BilliardsMatch>> GetMatchesAsync();
        Task<BilliardsMatch> GetSingleMatchAsync(int id);
        Task<IEnumerable<BilliardsMatch>> GetMatchesByUserAsync(int userId);
        Task<IEnumerable<BilliardsMatch>> GetMatchesByTypeAsync(int typeId);
        Task<IEnumerable<BilliardsMatch>> GetMatchesByModeAsync(int modeId);
        Task<IEnumerable<BilliardsMatch>> GetMatchesBySeasonAsync(int seasonNumberId);
        Task<IEnumerable<BilliardsMatch>> GetMatchesByTournamentAsync(int tournamentId);
        void InsertMatch(BilliardsMatch billiardsMatch);
        void DeleteMatch(BilliardsMatch billiardsMatch);
        void UpdateMatch(BilliardsMatch billiardsMatch);

    }
}