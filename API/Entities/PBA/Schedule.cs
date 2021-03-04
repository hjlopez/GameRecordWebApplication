namespace API.Entities.PBA
{
    public class Schedule
    {
        public int Id { get; set; }
        public int TeamA { get; set; }
        public int TeamB { get; set; }
        public ScheduleGroup ScheduleGroup { get; set; }
        public int GroupId { get; set; }
    }
}