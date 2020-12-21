using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("GameTypes")]
    public class GameTypes
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public Games Games { get; set; }
    }
}