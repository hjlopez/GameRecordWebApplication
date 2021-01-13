using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs.Billiards;
using API.Entities.Billiards;

namespace API.Interfaces.Billiards
{
    public interface ISeasonHistoryRepository
    {
        void InsertSeasonHistory(SeasonHistory seasonHistory);
        void DeleteSeasonHistory(SeasonHistory seasonHistory);
        Task<SeasonHistoryDto> GetSeasonHistory();
        Task<IEnumerable<SeasonHistory>> GetSeasonHistoryForChecking(int modeId, int seasonNumberId, int tournamentId, int typeId, 
                                                        int winId);
        Task<SeasonHistory> GetSeasonHistory(int userId, int seasonNumberId, int tournamentId, int typeId);
        Task<SeasonHistoryDto> GetSeasonHistoryBySeason(int seasonNumberId);
        Task<SeasonHistoryDto> GetSeasonHistoryByTournament(int tournamentId);
        Task<SeasonHistoryDto> GetSeasonHistoryByType(int typeId);

        Task<IEnumerable<SeasonHistoryDto>> GetSeasonRank(int tournamentId, int seasonNumberId, int typeId);
        
    }
}