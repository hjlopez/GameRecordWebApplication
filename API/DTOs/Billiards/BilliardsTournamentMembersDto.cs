namespace API.DTOs
{
    public class BilliardsTournamentMembersDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string GamerTag { get; set; }
        public int TournamentId { get; set; }
        public string TournamentName { get; set; }
        public int OpponentUserId { get; set; }
        public int Wins { get; set; } = 0;
        public int TypeWins { get; set; } = 0;
        public int SeasonWins { get; set; } = 0;
        public int NonPlayoffWins { get; set; } = 0;
        public int PlayoffWins { get; set; } = 0;
        public int TotalGamesPlayed { get; set; } = 0;
        public int SeasonNumberId { get; set; }
        public int TypeId { get; set; }
    }
}