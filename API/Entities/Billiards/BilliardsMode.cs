using System.Collections.Generic;

namespace API.Entities
{
    public class BilliardsMode
    {
        public int Id { get; set; }
        public string Mode { get; set; }
        
        public ICollection<TournamentMode> TournamentMode { get; set; }
        
    }
}