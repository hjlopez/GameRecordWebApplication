using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs.Billiards;
using API.Entities.Billiards;
using API.Helpers;

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
        Task<BilliardsMatch> CheckIfLastModeIsPlayed(int modeId, int seasonNumberId, int tournamentId, int typeId);
        Task<PagedList<BilliardsMatchDto>> GetMatchesByTournamentAsync(BilliardsMatchParams matchParams);
        Task<PagedList<BilliardsMatchDto>> GetFilteredMatches(BilliardsMatchParams matchParams);
        void InsertMatch(BilliardsMatch billiardsMatch);
        void DeleteMatch(BilliardsMatch billiardsMatch);
        void UpdateMatch(BilliardsMatch billiardsMatch);

    }
}