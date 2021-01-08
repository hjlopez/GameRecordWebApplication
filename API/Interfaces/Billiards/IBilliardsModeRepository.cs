using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs.Billiards;
using API.Entities;

namespace API.Interfaces.Billiards
{
    public interface IBilliardsModeRepository
    {
        Task<IEnumerable<BilliardsModeDto>> GetModesAsync();
        Task<BilliardsMode> GetModeByIdAsync(int id);
        Task<BilliardsMode> GetModeByName(string mode);
        void InsertMode(BilliardsMode dto);
        void UpdateMode(BilliardsMode dto);
        void DeleteMode(BilliardsMode dto);


        Task<IEnumerable<TournamentModeDto>> GetTournamentModesAsync(int tournamentId);
        Task<IEnumerable<TournamentModeDto>> GetTournamentModesAsyncWithName(int tournamentId);
        Task<TournamentMode> GetTournamentModeAsync(int tournamentId, int modeId);
        Task<TournamentMode> GetTournamentLastModeAsync(int tournamentId);
        Task<TournamentMode> GetTournamentModeAsync(int id);
        void InsertTournamentMode(TournamentMode mode);
        void DeleteTournamentMode(TournamentMode mode);
    }
}