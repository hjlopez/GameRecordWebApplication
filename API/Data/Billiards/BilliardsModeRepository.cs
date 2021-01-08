using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Billiards;
using API.Entities;
using API.Interfaces.Billiards;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Billiards
{
    public class BilliardsModeRepository : IBilliardsModeRepository
    {
        private readonly IMapper mapper;
        private readonly DataContext context;
        public BilliardsModeRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void DeleteMode(BilliardsMode mode)
        {
            context.BilliardsModes.Remove(mode);
        }

        public void DeleteTournamentMode(TournamentMode mode)
        {
            context.TournamentModes.Remove(mode);
        }

        public async Task<BilliardsMode> GetModeByIdAsync(int id)
        {
            return await context.BilliardsModes.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<BilliardsMode> GetModeByName(string mode)
        {
            return await context.BilliardsModes.Where(x => x.Mode.ToLower() == mode.ToLower()).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<BilliardsModeDto>> GetModesAsync()
        {
            var mode = await context.BilliardsModes.ToListAsync();

            return mapper.Map<IEnumerable<BilliardsModeDto>>(mode);
        }

        public async Task<TournamentMode> GetTournamentLastModeAsync(int tournamentId)
        {
            return await context.TournamentModes.Where(w => w.TournamentId == tournamentId && w.IsLast == true)
                .FirstOrDefaultAsync();
        }

        public async Task<TournamentMode> GetTournamentModeAsync(int tournamentId, int modeId)
        {
            return await context.TournamentModes.Where(w => w.TournamentId == tournamentId && w.ModeId == modeId)
                .SingleOrDefaultAsync();
        }

        public async Task<TournamentMode> GetTournamentModeAsync(int id)
        {
            return await context.TournamentModes.Where(w => w.Id == id).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TournamentModeDto>> GetTournamentModesAsync(int tournamentId)
        {
            var tour = await context.TournamentModes
                .Where(t => t.TournamentId == tournamentId)
                .OrderBy(x => x.Order)
                .ToListAsync();

            return mapper.Map<IEnumerable<TournamentModeDto>>(tour);
        }

        public async Task<IEnumerable<TournamentModeDto>> GetTournamentModesAsyncWithName(int tournamentId)
        {
            var tour = context.TournamentModes.AsQueryable();

            var finalResult = await tour
                .Where(t => t.TournamentId == tournamentId)
                .Include(m => m.BilliardsMode)
                .OrderBy(x => x.Order)
                .Select(s => new
                {
                    Id = s.Id,
                    TournamentId = s.TournamentId,
                    Order = s.Order,
                    IsLast = s.IsLast,
                    IsConsolation = s.IsConsolation,
                    HighestRank = s.HighestRank,
                    IsPlayoff = s.IsPlayoff,
                    ModeId = s.ModeId,
                    ModeName = s.BilliardsMode.Mode
                }).ToListAsync();

            var dto = new List<TournamentModeDto>();
            foreach (var item in finalResult)
            {
                dto.Add(new TournamentModeDto
                {
                    Id = item.Id,
                    TournamentId = item.TournamentId,
                    Order = item.Order,
                    IsLast = item.IsLast,
                    IsConsolation = item.IsConsolation,
                    HighestRank = item.HighestRank,
                    IsPlayoff = item.IsPlayoff,
                    ModeId = item.ModeId,
                    ModeName = item.ModeName
                });
            }

            return dto;
        }

        public void InsertMode(BilliardsMode mode)
        {
            context.BilliardsModes.Add(mode);
        }

        public void InsertTournamentMode(TournamentMode mode)
        {
            context.TournamentModes.Add(mode);
        }

        public void UpdateMode(BilliardsMode mode)
        {
            context.Entry(mode).State = EntityState.Modified;
        }
    }
}