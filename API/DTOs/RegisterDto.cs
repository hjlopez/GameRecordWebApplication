using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required] public string Username { get; set; }

        [Required] 
        [StringLength(20)]
        public string GamerTag { get; set; }
        [Required] public bool PlayMH { get; set; }
        [Required] public bool PlayDota { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required] 
        [StringLength(50, MinimumLength = 4)]
        public string Password { get; set; }
    }
}