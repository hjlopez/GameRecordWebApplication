using System.Threading.Tasks;
using API.Interfaces.Billiards;

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
        Task<bool> Complete();
        bool HasChanges();
    }
}