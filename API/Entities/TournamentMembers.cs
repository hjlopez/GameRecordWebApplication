using System.Collections.Generic;

namespace API.Entities
{
    public class TournamentMembers
    {
        public int Id { get; set; }
        public AppUser AppUser { get; set; }
        public int UserId { get; set; }
        public Tournament Tournament { get; set; }
        public int TournamentId { get; set; }
    }
}