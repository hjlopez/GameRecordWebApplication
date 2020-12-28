using System.Collections.Generic;
using API.Entities.Billiards;

namespace API.Entities
{
    public class BilliardsMatchType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<TournamentMatchType> TournamentMatchType { get; set; }
        public ICollection<SeasonHistory> SeasonHistories { get; set; }
    }
}