using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.PBA;
using API.Interfaces.PBA;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data.PBA
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public ScheduleRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ScheduleGroup>> GetAllScheduleGroup()
        {
            return await context.ScheduleGroups.ToListAsync();
        }

        public async Task<ScheduleGroup> GetDefaultScheduleGroup()
        {
            return await context.ScheduleGroups.Where(x => x.IsDefault).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Schedule>> GetScheduleByGroup(int groupId)
        {
            return await context.Schedules.Where(x => x.GroupId == groupId).ToListAsync();
        }

        public async Task<ScheduleGroup> GetScheduleGroup(int id)
        {
            return await context.ScheduleGroups.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<ScheduleGroup> GetScheduleGroup(string groupName)
        {
            return await context.ScheduleGroups.Where(x => x.GroupName == groupName).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ScheduleGroup>> GetSchedules()
        {
            return await context.ScheduleGroups.ToListAsync();
        }
    }
}