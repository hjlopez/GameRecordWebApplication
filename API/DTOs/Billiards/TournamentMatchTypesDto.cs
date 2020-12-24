namespace API.DTOs.Billiards
{
    public class TournamentMatchTypesDto
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int MatchTypeId { get; set; }
        public string MatchType { get; set; }
    }
}