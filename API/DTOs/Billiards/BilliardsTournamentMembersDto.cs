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
    }
}