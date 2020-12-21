using System.Collections;
using System.Collections.Generic;
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
    [Authorize(Policy = "RequireAdminRole")]

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

        [HttpPost("insert-billiards-tournament")]
        public async Task<ActionResult> InsertTournament(BilliardsTournamentDto dto)
        {
            // check if tournament name exists
            var exist = await unitOfWork.BilliardsTournamentRepository.GetTournamentDto(dto.TournamentName);
            if (exist != null) return BadRequest("Name already exists!");

            unitOfWork.BilliardsTournamentRepository.InsertTournament(dto);
            if (!await unitOfWork.Complete()) return BadRequest("Insert failed!");

            return Ok();
        }

        [HttpPut("update-billiards-tournament")]
        public async Task<ActionResult> UpdateTournament(BilliardsTournamentDto dto)
        {
            var id = await unitOfWork.BilliardsTournamentRepository.GetTournamentById(dto.Id);
            if (id == null) return BadRequest("Tournament name can't be found!");
            var tournament = await unitOfWork.BilliardsTournamentRepository.GetTournamentDto(id.TournamentName);

            mapper.Map(dto, tournament);
            unitOfWork.BilliardsTournamentRepository.UpdateTournament(tournament);
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Cannot update tournament name!");
        }

        [HttpDelete("delete-billiards-tournament/{tournamentId}")]
        public async Task<ActionResult> DeleteTournament(int tournamentId)
        {
            var id = await unitOfWork.BilliardsTournamentRepository.GetTournamentById(tournamentId);
            if (id == null) return BadRequest("Tournament name can't be found!");

            // check if tournament has members
            var members = await unitOfWork.BilliardsTournamentMembersRepository.GetMembersOfTournament(tournamentId);
            if (members == null) return BadRequest("There are already members of this tournament!");

            var tournament = await unitOfWork.BilliardsTournamentRepository.GetTournamentDto(id.TournamentName);

            unitOfWork.BilliardsTournamentRepository.DeleteTournament(tournament);
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Cannot delete tournament name!");
        }

        [HttpGet("get-billiards-tournament")]
        public async Task<ActionResult<IEnumerable<BilliardsTournamentDto>>> GetBilliardsTournaments()
        {
            var tournaments = await unitOfWork.BilliardsTournamentRepository.GetTournamentList();
            return Ok(tournaments);
        }

        [HttpGet("get-tournament-members/{tournamentId}")]
        public async Task<ActionResult<IEnumerable<BilliardsTournamentMembersDto>>> GetMembers(int tournamentId)
        {
            var tour = await unitOfWork.BilliardsTournamentRepository.GetTournamentById(tournamentId);
            if (tour == null) return BadRequest("Selected tournament doesn't exist!");

            var members = await unitOfWork.BilliardsTournamentMembersRepository.GetMembersOfTournament(tournamentId);
            // get username
            foreach (var member in members)
            {
                var user = await unitOfWork.UserRepository.GetUserByIdAsync(member.UserId);
                member.Username = user.UserName;
                member.GamerTag = user.GamerTag;
            }
            
            return Ok(members);
        }
        
        [HttpPost("insert-tournament-members")]
        public async Task<ActionResult> InsertMembers(BilliardsTournamentMembersDto dto)
        {
            var tour = await unitOfWork.BilliardsTournamentRepository.GetTournamentById(dto.TournamentId);
            if (tour == null) return BadRequest("Selected tournament doesn't exist!");

            var user = await unitOfWork.UserRepository.GetUserByIdAsync(dto.UserId);
            if (user == null) return BadRequest("User doesn't exist!");
            if (user.UserName == "admin") return BadRequest("Admin cannot be added!");

            // already a member
            var alreadyMember = await unitOfWork.BilliardsTournamentMembersRepository.GetMemberOfTournament(dto.TournamentId, dto.UserId);
            if (alreadyMember != null) return BadRequest("User is already a member!");

            unitOfWork.BilliardsTournamentMembersRepository.InsertMemberInTournament(mapper.Map<TournamentMembers>(dto));
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Unable to add user to tournament");
        }

        [HttpDelete("delete-tournament-members/{id}")]
        public async Task<ActionResult> DeleteMembers(int id)
        {
            var member = await unitOfWork.BilliardsTournamentMembersRepository.GetMemberOfTournament(id);
            if (member == null) return BadRequest("User is not a member!");

            var tour = await unitOfWork.BilliardsTournamentRepository.GetTournamentById(member.TournamentId);
            if (tour == null) return BadRequest("Tournament doesn't exist!");

            var user = await unitOfWork.UserRepository.GetUserByIdAsync(member.UserId);
            if (user == null) return BadRequest("User doesn't exist!");            

            // ------------------------------------------------- add checker that member can't be removed if there is games played
            
            unitOfWork.BilliardsTournamentMembersRepository.RemoveMemberFromTournament(member);
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Unable to remove user to tournament");
        }

        [HttpGet("get-user/{knownAs}")]
        public async Task<ActionResult<UserDto>> GetUserByKnownAs(string knownAs)
        {
            var user = await unitOfWork.UserRepository.GetUserByKnownAs(knownAs);
            if (user == null) return BadRequest("No user with that username or tag");

            return mapper.Map<UserDto>(user);
        }
    }
}