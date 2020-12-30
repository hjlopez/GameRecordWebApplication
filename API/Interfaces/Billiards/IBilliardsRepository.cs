using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.Billiards;
using API.Entities;
using API.Entities.Billiards;

namespace API.Interfaces.Billiards
{
    public interface IBilliardsRepository
    {
        Task<IEnumerable<BilliardsTournamentDto>> GetTournamentsForUserAsync(int userId);
        Task<IEnumerable<Season>> GetSeasonsForTournamentsAsync(int tournamentId);
        Task<Season> GetSeasonForTournamentAsync(int id);
        Task<Season> GetSeasonBySeasonNumber(int seasonNumber);
        Task<Season> GetSeasonBySeasonNumberId(int seasonNumberId);
        void InsertSeasonForTournament(Season season);
        void DeleteSeasonFromTournament(Season season);
        void UpdateSeason(Season season);

        Task<IEnumerable<SeasonHistory>> GetHistoryAsync();
        Task<IEnumerable<SeasonHistory>> GetHistoryBySeasonAsync(int seasonId);
        Task<IEnumerable<SeasonHistory>> GetHistoryByTournamentAsync(int tournamentId);
        Task<IEnumerable<SeasonHistory>> GetHistoryByTypeAsync(int typeId);
        Task<IEnumerable<SeasonHistory>> GetHistoryByUserAsync(int userId);
        void InsertSeasonHistory(SeasonHistory SeasonHistory);
        void DeleteSeasonHistory(SeasonHistory SeasonHistory);

    }
}