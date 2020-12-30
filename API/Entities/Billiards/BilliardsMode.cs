using System.Collections.Generic;
using API.Entities.Billiards;

namespace API.Entities
{
    public class BilliardsMode
    {
        public int Id { get; set; }
        public string Mode { get; set; }
        
        public ICollection<TournamentMode> TournamentMode { get; set; }
        public ICollection<BilliardsMatch> BilliardsMatches { get; set; }
        
    }
}