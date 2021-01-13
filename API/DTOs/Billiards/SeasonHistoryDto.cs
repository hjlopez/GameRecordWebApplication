namespace API.DTOs.Billiards
{
    public class SeasonHistoryDto
    {
        public int Id { get; set; }
        public int SeasonNumberId { get; set; }
        public int SeasonNumber { get; set; }
        public int TournamentId { get; set; }
        public string TournamentName { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int Rank { get; set; }
        public int MatchId { get; set; }
        public int ModeId { get; set; }
        public bool IsDone { get; set; }
        public string Url { get; set; }
        public int Wins { get; set; }
    }
}