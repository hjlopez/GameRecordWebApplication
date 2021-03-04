using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs.PBA;
using API.Entities.PBA;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PBA
{
    [Authorize]
    public class ScheduleController : BaseApiController
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ScheduleController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("get-groups")]
        public async Task<ActionResult<ScheduleGroup>> GetScheduleGroups()
        {
            return Ok(await unitOfWork.ScheduleRepository.GetAllScheduleGroup());
        }

        [HttpGet("get-selected-sched/{groupId}")]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetSelectedSchedule(int groupId)
        {
            var s = await unitOfWork.ScheduleRepository.GetScheduleByGroup(groupId);
            return Ok(mapper.Map<IEnumerable<ScheduleDto>>(s));
        }
    }
}