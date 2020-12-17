using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public AdminController(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("get-users")]
        public async Task<ActionResult<AppUser>> GetUsers()
        {
            var users = await userManager.Users
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.Role)
                .OrderBy(u => u.Id)
                .Select(u => new
                {
                    u.Id,
                    Username = u.UserName,
                    GamerTag = u.GamerTag,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList(),
                    PlayMH = u.PlayMH,
                    PlayDota = u.PlayDota,
                    JoinBilliards = u.JoinBilliards,
                    Email = u.Email
                })
                .ToListAsync();

            return Ok(users);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("delete-user/{username}")]
        public async Task<ActionResult> DeleteUser(string username)
        {
            var user = await unitOfWork.UserRepository.GetUserByUsernameAsync(username);
            if (user == null) return BadRequest("User is non-existent!");

            unitOfWork.UserRepository.DeleteUser(user);
            if (!await unitOfWork.Complete()) return BadRequest("There was an error in deleting the user");

            return Ok(new { message = "User deleted!" });
            // return BadRequest("There was an error in deleting the user");
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("update-user")]
        public async Task<ActionResult> UpdateUser(AdminUserUpdateDto admin)
        {
            var username = await unitOfWork.UserRepository.GetUserByIdAsync(admin.Id);
            var user = await unitOfWork.UserRepository.GetUserByUsernameAsync(username.UserName);

            // validations
            if (user == null) return BadRequest("Cannot find selected user!");
            if (user.UserName == "admin") return BadRequest("Cannot update the admin user!");

            mapper.Map(admin, user);

            // update user
            unitOfWork.UserRepository.Update(user);
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Cannot update selected user!");
        }
    }
}