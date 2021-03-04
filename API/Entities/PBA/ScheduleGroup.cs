using System.Collections;
using System.Collections.Generic;

namespace API.Entities.PBA
{
    public class ScheduleGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public bool IsDefault { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
}