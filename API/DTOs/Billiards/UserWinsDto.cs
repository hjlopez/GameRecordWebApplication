namespace API.DTOs.Billiards
{
    public class UserWinsDto
    {
        public int UserId { get; set; }
        public int UserWins { get; set; } = 0;
        public int OpponentUserId { get; set; }
        public int OpponentWins { get; set; } = 0;
        public int TotalGamesPlayed { get; set; }
        public int TournamentId { get; set; }
        public int TypeId { get; set; } = 0;
        public int ModeId { get; set; } = 0;
        public int SeasonNumberId { get; set; } = 0;
    }
}