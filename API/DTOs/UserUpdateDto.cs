namespace API.DTOs
{
    public class UserUpdateDto
    {
        public string Username { get; set; }
        public string GamerTag { get; set; }
        public bool PlayMH { get; set; }
        public bool PlayDota { get; set; }
        public string Email { get; set; }
    }
}