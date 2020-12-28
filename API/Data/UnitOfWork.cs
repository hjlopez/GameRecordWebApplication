using System.Threading.Tasks;
using API.Data.Billiards;
using API.Interfaces;
using API.Interfaces.Billiards;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public UnitOfWork(DataContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public IUserRepository UserRepository => new UserRepository(context, mapper);

        public IGamesRepository GamesRepository => new GamesRepository(context, mapper);

        public IBilliardsTournamentRepository BilliardsTournamentRepository => new BilliardsTournamentRepository(context, mapper);

        public IBilliardsTournamentMembersRepository BilliardsTournamentMembersRepository => new BilliardsTournamentMembersRepository(context, mapper);

        public IBilliardsMatchTypesRepository BilliardsMatchTypesRepository => new BilliardsMatchTypesRepository(context, mapper);

        public IBilliardsModeRepository BilliardsModeRepository => new BilliardsModeRepository(context, mapper);

        public IBilliardsRepository BilliardsRepository => new BilliardsRepository(context, mapper);

        public async Task<bool> Complete()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            // returns true if there are changes
            return context.ChangeTracker.HasChanges(); 
        }
    }
}