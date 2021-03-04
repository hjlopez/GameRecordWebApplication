using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs.PBA;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.PBA
{
    [Authorize]
    public class TeamController : BaseApiController
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public TeamController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeams()
        {
            var teams = await unitOfWork.TeamRepository.GetTeamsAsync();
            return Ok(mapper.Map<IEnumerable<TeamDto>>(teams));
        }
    }
}