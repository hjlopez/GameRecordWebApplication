namespace API.DTOs
{
    public class GamesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LoggedOutLink { get; set; }
        public string LoggedInLink { get; set; }
    }
}