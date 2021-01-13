using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs.Billiards;

namespace API.Interfaces.Billiards
{
    public interface IUserWins
    {
        Task<UserWinsDto> GetUserTotalWins(int userId);
        Task<UserWinsDto> GetUserTotalWinsSeasonType(int userId, int seasonNumberId, int typeId);
        Task<UserWinsDto> GetTotalUserWinsVsUser(int userId, int opponentUserId);
        Task<UserWinsDto> GetTotalUserWinsVsUserTournament(int userId, int opponentUserId, int tournamentId);
        Task<UserWinsDto> GetTotalUserWinsVsUserSeason(int userId, int opponentUserId, int tournamentId, int seasonId);
        Task<UserWinsDto> GetTotalUserWinsVsUserType(int userId, int opponentUserId, int tournamentId, int seasonId, int typeId);

        Task<IEnumerable<BilliardsMatchDto>> GetAllMatchesVsUser(int userId, int opponentUserId, int tournamentId);
        Task<IEnumerable<BilliardsMatchDto>> GetSeasonMatchesVsUser(int userId, int opponentUserId, int tournamentId, int seasonNumberId);
        Task<IEnumerable<BilliardsMatchDto>> GetTypeMatchesVsUser(int userId, int opponentUserId, int tournamentId, int typeId);
        Task<IEnumerable<BilliardsMatchDto>> GetNonPlayoffMatchesVsUser(int userId, int opponentUserId, int tournamentId);
        Task<IEnumerable<BilliardsMatchDto>> GetPlayoffMatchesVsUser(int userId, int opponentUserId, int tournamentId);
    }
}