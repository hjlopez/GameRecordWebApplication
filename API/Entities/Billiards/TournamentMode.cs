using System.Collections.Generic;

namespace API.Entities
{
    public class TournamentMode
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        
        public int Order { get; set; }
        public bool IsLast { get; set; }
        public bool IsConsolation { get; set; }
        public int HighestRank { get; set; } // highest rank can be obtained -> for last and consolation only
        public bool IsPlayoff { get; set; }
        public BilliardsMode BilliardsMode { get; set; }
        public int ModeId { get; set; }
    }
}