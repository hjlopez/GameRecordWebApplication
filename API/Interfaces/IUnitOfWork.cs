using System.Threading.Tasks;
using API.Interfaces.Billiards;
using API.Interfaces.PBA;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository {get;}
        IGamesRepository GamesRepository {get;}
        IBilliardsTournamentRepository BilliardsTournamentRepository {get;}
        IBilliardsTournamentMembersRepository BilliardsTournamentMembersRepository {get;}
        IBilliardsMatchTypesRepository BilliardsMatchTypesRepository {get;}
        IBilliardsModeRepository BilliardsModeRepository {get;}
        IBilliardsRepository BilliardsRepository {get;}
        IBilliardsGameRepository BilliardsGameRepository {get;}
        IUserWins UserWins {get;}
        ISeasonHistoryRepository SeasonHistoryRepository {get;}
        ITeamRepository TeamRepository {get;}
        IScheduleRepository ScheduleRepository {get;}
        Task<bool> Complete();
        bool HasChanges();
    }
}