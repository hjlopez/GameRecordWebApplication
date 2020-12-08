using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class RegisterDto
    {
        [Required] public string Username { get; set; }
        [Required] public string GamerTag { get; set; }
        [Required] public bool PlayMH { get; set; }
        [Required] public bool PlayDota { get; set; }

        [Required] 
        [StringLength(50, MinimumLength = 4)]
        public string Password { get; set; }
    }
}