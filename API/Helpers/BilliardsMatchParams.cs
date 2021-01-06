namespace API.Helpers
{
    public class BilliardsMatchParams : PaginationParams
    {
        public int TournamentId { get; set; }
        public int TypeId { get; set; } = 0;
        public int ModeId { get; set; } = 0;
        public int SeasonNumberId { get; set; } = 0;
    }
}