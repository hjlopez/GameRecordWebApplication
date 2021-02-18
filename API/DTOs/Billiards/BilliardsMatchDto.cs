using System;

namespace API.DTOs.Billiards
{
    public class BilliardsMatchDto
    {
        public int Id { get; set; }
        public int WinUserId { get; set; }
        public int LoseUserId { get; set; }
        public string WinPhotoUrl { get; set; }
        public string LosePhotoUrl { get; set; }
        public int TypeId { get; set; }
        public int ModeId { get; set; }
        public int SeasonNumberId { get; set; }
        public int TournamentId { get; set; }
        public int WinnerWins { get; set; }
        public int LoserWins { get; set; }
        public int TotalGamesPlayed { get; set; }
        public DateTime DatePlayed { get; set; }
    }
}