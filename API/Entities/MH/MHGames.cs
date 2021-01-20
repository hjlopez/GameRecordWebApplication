namespace API.Entities.MH
{
    public class MHGames
    {
        public int Id { get; set; }
        public int GenerationNumber { get; set; }
        public string GameName { get; set; }
        public string ShortName { get; set; }
        public string JapaneseName { get; set; }
        public string JapShortName { get; set; }
        public string FlagshipMonster { get; set; }
        public int InitialYearRelease { get; set; }
        public string IconUrl { get; set; }
        public string FlagshipIconUrl { get; set; }
    }
}