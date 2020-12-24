namespace API.DTOs.Billiards
{
    public class TournamentModeDto
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int Order { get; set; }
        public bool IsLast { get; set; }
        public bool IsConsolation { get; set; }
        public int HighestRank { get; set; }
        public bool IsPlayoff { get; set; }
        public int ModeId { get; set; }
    }
}