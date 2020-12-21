using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class BilliardsTournamentMembersRepository : IBilliardsTournamentMembersRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public BilliardsTournamentMembersRepository(DataContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<TournamentMembers> GetMemberOfTournament(int id)
        {
            var query = context.TournamentMembers.AsQueryable();
            var member = await query.Where(t => t.Id == id).FirstOrDefaultAsync();

            return member;
        }

        public async Task<TournamentMembers> GetMemberOfTournament(int tournamentId, int userId)
        {
            var query = context.TournamentMembers.AsQueryable();
            var member = await query.Where(t => t.TournamentId == tournamentId && t.UserId == userId).FirstOrDefaultAsync();

            return member;
        }

        public async Task<IEnumerable<BilliardsTournamentMembersDto>> GetMembersOfTournament(int tournamentId)
        {
            var query = context.TournamentMembers.AsQueryable();
            var members = await query.Where(t => t.TournamentId == tournamentId).ToListAsync();

            return mapper.Map<IEnumerable<BilliardsTournamentMembersDto>>(members);
        }

        public void InsertMemberInTournament(TournamentMembers member)
        {
            context.TournamentMembers.Add(member);
        }

        public void RemoveMemberFromTournament(TournamentMembers member)
        {
            context.TournamentMembers.Remove(member);
        }
    }
}