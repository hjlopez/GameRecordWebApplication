using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class AdminUserUpdateDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string GamerTag { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public bool PlayMH { get; set; }
        public bool PlayDota { get; set; }
        public bool JoinBilliards { get; set; }
    }
}