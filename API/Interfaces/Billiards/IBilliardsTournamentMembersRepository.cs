using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IBilliardsTournamentMembersRepository
    {
        Task<IEnumerable<BilliardsTournamentMembersDto>> GetMembersOfTournament(int tournamentId);
        Task<TournamentMembers> GetMemberOfTournament(int tournamentId, int userId);
        Task<TournamentMembers> GetMemberOfTournament(int id);
        void InsertMemberInTournament(TournamentMembers dto);
        void RemoveMemberFromTournament(TournamentMembers dto);
    }
}