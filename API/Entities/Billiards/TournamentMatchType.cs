using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class TournamentMatchType
    {
        public int Id { get; set; }
        public Tournament Tournament { get; set; }
        public int TournamentId { get; set; }
        public BilliardsMatchType BilliardsMatchTypes { get; set; }
        public int MatchTypeId { get; set; }
    }
}