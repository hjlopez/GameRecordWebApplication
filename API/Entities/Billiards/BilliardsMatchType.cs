using System.Collections.Generic;

namespace API.Entities
{
    public class BilliardsMatchType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<TournamentMatchType> TournamentMatchType { get; set; }
    }
}