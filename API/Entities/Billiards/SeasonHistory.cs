using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Billiards
{
    [Table("SeasonHistory")]
    public class SeasonHistory
    {
        public int Id { get; set; }
        public Season Season { get; set; }
        public int SeasonNumberId { get; set; }
        public Tournament Tournament { get; set; }
        public int TournamentId { get; set; }
        public BilliardsMatchType BilliardsMatchType { get; set; }
        public int TypeId { get; set; }
        public AppUser AppUser { get; set; }
        public int UserId { get; set; }
        public int Rank { get; set; }

        public bool IsDone { get; set; }

    }
}