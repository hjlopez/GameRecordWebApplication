using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Billiards
{
    public class Season
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SeasonNumber { get; set; }
        public Tournament Tournament { get; set; }
        public int TournamentId { get; set; }
        public bool IsDone { get; set; }

        public ICollection<SeasonHistory> SeasonHistories { get; set; }
    }
}