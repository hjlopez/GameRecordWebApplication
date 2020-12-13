using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ITokenService tokenService;
        public UsersController(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
        {
            this.tokenService = tokenService;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await unitOfWork.UserRepository.GetUsersAsync();

            return Ok(users);
        }

        [HttpPut]
        public async Task<ActionResult<UserDto>> UpdateUser(UserUpdateDto userUpdateDto)
        {
            var user = await unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            mapper.Map(userUpdateDto, user);
            unitOfWork.UserRepository.Update(user);
            if (await unitOfWork.Complete())
            {
                return new UserDto
                {
                    Username = user.UserName,
                    Token = await tokenService.CreateToken(user),
                    GamerTag = user.GamerTag,
                    PlayMH = user.PlayMH,
                    PlayDota = user.PlayDota,
                    JoinBilliards = user.JoinBilliards
                };
                
            };

            return BadRequest("Failed to update user!");
        }

        [HttpPost("reset")]
        public async Task<ActionResult> ResetPassword()
        {
            var users = await unitOfWork.UserRepository.GetUsersAsync();

            return NoContent();
        }
    }
}