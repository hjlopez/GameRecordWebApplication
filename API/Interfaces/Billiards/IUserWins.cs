using System.Threading.Tasks;
using API.DTOs.Billiards;

namespace API.Interfaces.Billiards
{
    public interface IUserWins
    {
        Task<UserWinsDto> GetUserTotalWins(int userId);
        Task<UserWinsDto> GetTotalUserWinsVsUser(int userId, int opponentUserId);
        Task<UserWinsDto> GetTotalUserWinsVsUserTournament(int userId, int opponentUserId, int tournamentId);
        Task<UserWinsDto> GetTotalUserWinsVsUserSeason(int userId, int opponentUserId, int tournamentId, int seasonId);
        Task<UserWinsDto> GetTotalUserWinsVsUserType(int userId, int opponentUserId, int tournamentId, int seasonId, int typeId);
        // Task<UserWinsDto> GetTotalUserWinsVsUserType(UserWinsDto dto);
    }
}