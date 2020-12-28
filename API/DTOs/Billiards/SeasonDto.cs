namespace API.DTOs.Billiards
{
    public class SeasonDto
    {
        public int Id { get; set; }
        public int SeasonNumber { get; set; }
        public int TournamentId { get; set; }
        public string TournamentName { get; set; }
        public bool IsDone { get; set; }
    }
}
