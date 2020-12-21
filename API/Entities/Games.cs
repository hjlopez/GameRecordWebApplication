using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Games")]
    public class Games
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string LoggedOutLink { get; set; }

        [Required]
        public string LoggedInLink { get; set; }
        public GameTypes GameTypes { get; set; }
        public int GameTypesId { get; set; }
    }
}