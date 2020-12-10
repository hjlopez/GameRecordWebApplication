namespace API.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string PhotoUrl { get; set; }
        public string GamerTag { get; set; }
        public bool PlayMH { get; set; }
        public bool PlayDota { get; set; }
        public string Email { get; set; }
    }
}