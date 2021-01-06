using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IBilliardsTournamentRepository
    {
        Task<IEnumerable<BilliardsTournamentDto>> GetTournamentList();
        Task<IEnumerable<BilliardsTournamentMembersDto>> GetMemberTournamentList();
        Task<IEnumerable<BilliardsTournamentMembersDto>> GetMembersOfTournament(int tournamentId);
        Task<Tournament> GetTournamentDto(string tournamentName);
        Task<Tournament> GetTournamentById(int id);
        void InsertTournament(BilliardsTournamentDto dto);
        void UpdateTournament(Tournament tournament);
        void DeleteTournament(Tournament tournament);
    }
}