using System.Collections.Generic;
using API.Entities.Billiards;

namespace API.Entities
{
    public class Tournament
    {
        public int Id { get; set; }
        public string TournamentName { get; set; }
        public ICollection<TournamentMembers> TournamentMembers { get; set; }
        public ICollection<TournamentMatchType> TournamentMatchTypes { get; set; }
        public Season Season { get; set; }
        public ICollection<SeasonHistory> SeasonHistories { get; set; }
    }
}