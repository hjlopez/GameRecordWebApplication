using System.Threading.Tasks;
using API.DTOs.Billiards;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Billiards
{
    public class UserWinsController : BaseApiController
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public UserWinsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet("get-season-type")]
        // public async Task<ActionResult<UserWinsDto>> GetSeasonTypeMatchUp(UserWinsDto userWinsDto)
        public async Task<ActionResult<UserWinsDto>> GetSeasonTypeMatchUp([FromQuery] int userId, int opponentUserId, int typeId,
                                                                                int seasonNumberId, int tournamentId)
        {      
            // return Ok();     
            return Ok(await unitOfWork.UserWins.GetTotalUserWinsVsUserType(userId, opponentUserId, tournamentId, seasonNumberId, typeId));
        }
    }
}