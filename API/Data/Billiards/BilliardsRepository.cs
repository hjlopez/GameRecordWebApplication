using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Entities.Billiards;
using API.Interfaces.Billiards;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Billiards
{
    public class BilliardsRepository : IBilliardsRepository
    {
        private readonly IMapper mapper;
        private readonly DataContext context;
        public BilliardsRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public void DeleteSeasonFromTournament(Season season)
        {
            context.Seasons.Remove(season);
        }

        public void DeleteSeasonHistory(SeasonHistory SeasonHistory)
        {
            context.SeasonHistories.Remove(SeasonHistory);
        }

        public async Task<IEnumerable<SeasonHistory>> GetHistoryAsync()
        {
            return await context.SeasonHistories.ToListAsync();
        }

        public async Task<IEnumerable<SeasonHistory>> GetHistoryBySeasonAsync(int seasonNumberId)
        {
            return await context.SeasonHistories.Where(s => s.SeasonNumberId == seasonNumberId).ToListAsync();
        }

        public async Task<IEnumerable<SeasonHistory>> GetHistoryByTournamentAsync(int tournamentId)
        {
            return await context.SeasonHistories.Where(s => s.TournamentId == tournamentId).ToListAsync();
        }

        public async Task<IEnumerable<SeasonHistory>> GetHistoryByTypeAsync(int typeId)
        {
            return await context.SeasonHistories.Where(s => s.TypeId == typeId).ToListAsync();
        }

        public async Task<IEnumerable<SeasonHistory>> GetHistoryByUserAsync(int userId)
        {
            return await context.SeasonHistories.Where(s => s.UserId == userId).ToListAsync();
        }

        public async Task<Season> GetSeasonBySeasonNumber(int seasonNumber)
        {
            return await context.Seasons.Where(s => s.SeasonNumber == seasonNumber).SingleOrDefaultAsync();
        }

        public async Task<Season> GetSeasonBySeasonNumberId(int seasonNumberId)
        {
            return await context.Seasons.Where(s => s.Id == seasonNumberId).SingleOrDefaultAsync();
        }

        public async Task<Season> GetSeasonForTournamentAsync(int id)
        {
            return await context.Seasons.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Season>> GetSeasonsForTournamentsAsync(int tournamentId)
        {
            return await context.Seasons.Where(t => t.TournamentId == tournamentId)
                .OrderByDescending(s => s.SeasonNumber)
                .ToListAsync();
            
        }

        public async Task<IEnumerable<BilliardsTournamentDto>> GetTournamentsForUserAsync(int userId)
        {
            if (userId == 0)
            {
                var tours = await context.Tournament.ToListAsync();
                return mapper.Map<IEnumerable<BilliardsTournamentDto>>(tours);
            }
            else
            {
                var tours = context.TournamentMembers.AsQueryable();
                var returnValue = tours
                                .Where(x => x.UserId == userId)
                                .Include(t => t.Tournament)
                                .Select(s => new {
                                    Id = s.TournamentId,
                                    TournamentName = s.Tournament.TournamentName
                                }).ToListAsync();

                var x = new List<BilliardsTournamentDto>();

                foreach (var value in returnValue.Result)
                {
                    x.Add(new BilliardsTournamentDto {Id = value.Id, TournamentName = value.TournamentName});
                }
                return x;
            }

        }

        public void InsertSeasonForTournament(Season season)
        {
            context.Seasons.Add(season);
        }

        public void InsertSeasonHistory(SeasonHistory SeasonHistory)
        {
            context.SeasonHistories.Add(SeasonHistory);
        }

        public void UpdateSeason(Season season)
        {
            context.Entry(season).State = EntityState.Modified;
        }
    }
}