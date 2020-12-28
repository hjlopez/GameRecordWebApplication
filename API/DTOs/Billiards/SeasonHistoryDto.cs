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
        public int Rank { get; set; }
        public bool IsDone { get; set; }
    }
}