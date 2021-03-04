using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities.PBA;

namespace API.Interfaces.PBA
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<ScheduleGroup>> GetSchedules();
        Task<IEnumerable<ScheduleGroup>> GetAllScheduleGroup();
        Task<ScheduleGroup> GetScheduleGroup(int id);
        Task<ScheduleGroup> GetScheduleGroup(string groupName);
        Task<ScheduleGroup> GetDefaultScheduleGroup();
        Task<IEnumerable<Schedule>> GetScheduleByGroup(int groupId);
    }
}