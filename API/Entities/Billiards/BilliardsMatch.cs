using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Billiards
{
    [Table("BilliardsMatch")]
    public class BilliardsMatch
    {
        public int Id { get; set; }
        public AppUser AppUserWin { get; set; }
        public int WinUserId { get; set; }
        public AppUser AppUserLose { get; set; }
        public int LoseUserId { get; set; }
        public BilliardsMatchType BilliardsMatchType { get; set; }
        public int TypeId { get; set; }
        public BilliardsMode BilliardsMode { get; set; }
        public int ModeId { get; set; }
        public Season Season { get; set; }
        public int SeasonNumberId { get; set; }
        public Tournament Tournament { get; set; }
        public int TournamentId { get; set; }

        public int WinnerWins { get; set; }
        public int LoserWins { get; set; }
        public int TotalGamesPlayed { get; set; }


    }
}