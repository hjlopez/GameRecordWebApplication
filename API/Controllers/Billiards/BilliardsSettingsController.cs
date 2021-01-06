using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.Billiards;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Games_Settings
{
    [Authorize(Policy = "RequireAdminRole")]
    public class BilliardsSettingsController : BaseApiController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        
        public BilliardsSettingsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
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
            

            //check if tournament has modes

            var tournament = await unitOfWork.BilliardsTournamentRepository.GetTournamentDto(id.TournamentName);

            unitOfWork.BilliardsTournamentRepository.DeleteTournament(tournament);
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Cannot delete tournament name!");
        }

        [AllowAnonymous]
        [HttpGet("get-billiards-tournament")]
        public async Task<ActionResult<IEnumerable<BilliardsTournamentDto>>> GetBilliardsTournaments()
        {
            var tournaments = await unitOfWork.BilliardsTournamentRepository.GetTournamentList();
            
            return Ok(tournaments);
        }

        [AllowAnonymous]
        [HttpGet("get-billiards-tournament-member")]
        public async Task<ActionResult<IEnumerable<BilliardsTournamentDto>>> GetBilliardsTournamentsMember()
        {
            var tournaments = await unitOfWork.BilliardsTournamentRepository.GetMemberTournamentList();
            
            return Ok(tournaments);
        }

        [AllowAnonymous]
        [Authorize]
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

        [HttpGet("get-match-types")]
        public async Task<ActionResult<IEnumerable<BilliardsMatchTypeDto>>> GetMatchTypes()
        {
            var types = await unitOfWork.BilliardsMatchTypesRepository.GetMatchTypesAsync();

            return Ok(mapper.Map<IEnumerable<BilliardsMatchTypeDto>>(types));
        }

        [HttpGet("get-match-type/{type}")]
        public async Task<ActionResult<BilliardsMatchTypeDto>> GetMatchType(string type)
        {
            var types = await unitOfWork.BilliardsMatchTypesRepository.GetMatchTypeByNameAsync(type);
            if (types == null) return BadRequest("No such type exists!");

            return Ok(mapper.Map<BilliardsMatchTypeDto>(types));
        }

        [HttpPost("insert-match-types")]
        public async Task<ActionResult> InsertMatchTypes(BilliardsMatchTypeDto dto)
        {
            var type = await unitOfWork.BilliardsMatchTypesRepository.GetMatchTypeByNameAsync(dto.Type);
            if (type != null) return BadRequest("Already exists!");

            unitOfWork.BilliardsMatchTypesRepository.InsertMatchType(mapper.Map<BilliardsMatchType>(dto));
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Insert failed!");
        }

        [HttpPut("update-match-types")]
        public async Task<ActionResult> UpdateMatchTypes(BilliardsMatchTypeDto dto)
        {
            var type = await unitOfWork.BilliardsMatchTypesRepository.GetMatchTypeByIdAsync(dto.Id);
            if (type == null) return BadRequest("Type does not exists!");

            mapper.Map(dto, type);
            unitOfWork.BilliardsMatchTypesRepository.UpdateMatchType(type);
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Updated failed");
        }

        [HttpDelete("delete-match-types/{id}")]
        public async Task<ActionResult> DeleteMatchTypes(int id)
        {
            var type = await unitOfWork.BilliardsMatchTypesRepository.GetMatchTypeByIdAsync(id);
            if (type == null) return BadRequest("Type does not exists!");

            // check if it's in tournament already
            var exist = await unitOfWork.BilliardsMatchTypesRepository.GetTournamentMatchTypeByMatchIdAsync(id);
            if (exist != null) return BadRequest("This type is being used by a tournament!");

            unitOfWork.BilliardsMatchTypesRepository.DeleteMatchType(type);
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Delete failed");
        }

        [AllowAnonymous]
        [Authorize]
        [HttpGet("get-tournament-types/{tournamentId}")]
        public async Task<ActionResult<IEnumerable<TournamentMatchTypesDto>>> GetTournamentTypes(int tournamentId)
        {
            var tour = await unitOfWork.BilliardsTournamentRepository.GetTournamentById(tournamentId);
            if (tour == null) return BadRequest("Tournament doesn't exist!");

            var types = await unitOfWork.BilliardsMatchTypesRepository.GetTournamentMatchTypes(tournamentId);
            foreach(var type in types)
            {
                var s = await unitOfWork.BilliardsMatchTypesRepository.GetMatchTypeByIdAsync(type.MatchTypeId);
                type.MatchType = s.Type;
            }

            return Ok(types);
        }

        [HttpPost("insert-tournament-types")]
        public async Task<ActionResult> AddTournamentTypes(TournamentMatchTypesDto dto)
        {
            var tour = await unitOfWork.BilliardsTournamentRepository.GetTournamentById(dto.TournamentId);
            if (tour == null) return BadRequest("Tournament doesn't exist!");

            var type = await unitOfWork.BilliardsMatchTypesRepository.GetMatchTypeByIdAsync(dto.MatchTypeId);
            if (type == null) return BadRequest("Type doesn't exist!");

            var exists = await unitOfWork.BilliardsMatchTypesRepository.GetTournamentMatchType(dto.MatchTypeId, dto.TournamentId);
            if (exists != null) return BadRequest("Type is already included!");

            unitOfWork.BilliardsMatchTypesRepository.InsertTournamentMatchTypes(mapper.Map<TournamentMatchType>(dto));
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Insert failed");
        }

        [HttpDelete("delete-tournament-types/{id}")]
        public async Task<ActionResult> DeleteTournamentTypes(int id)
        {
            var tourType = await unitOfWork.BilliardsMatchTypesRepository.GetTournamentMatchType(id);
            if (tourType == null) return BadRequest("Type doesn't exist in tournament!");

            // -------------------------------------------------- add checker if it's already used in match record history
            var mt = mapper.Map<TournamentMatchType>(tourType);
            unitOfWork.BilliardsMatchTypesRepository.RemoveTournamentMatchTypes(mt);
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Delete failed");
        }

        [HttpGet("get-modes")]
        public async Task<ActionResult<IEnumerable<TournamentMatchTypesDto>>> GetModes()
        {
            var modes = await unitOfWork.BilliardsModeRepository.GetModesAsync();
            return Ok(modes);
        }

        [HttpPost("insert-modes")]
        public async Task<ActionResult> InsertModes(BilliardsModeDto dto)
        {
            var mode = await unitOfWork.BilliardsModeRepository.GetModeByName(dto.Mode);
            if (mode != null) return BadRequest("Mode already exist!");

            unitOfWork.BilliardsModeRepository.InsertMode(mapper.Map<BilliardsMode>(dto));
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Insert failed");
        }

        [HttpPut("update-modes")]
        public async Task<ActionResult> UpdateModes(BilliardsModeDto dto)
        {
            var mode = await unitOfWork.BilliardsModeRepository.GetModeByIdAsync(dto.Id);
            if (mode == null) return BadRequest("Mode doesn't exist!");

            mapper.Map(dto, mode);
            unitOfWork.BilliardsModeRepository.UpdateMode(mode);
            if (await unitOfWork.Complete()) return NoContent();
            
            return BadRequest("Update failed");
        }

        [HttpDelete("delete-modes/{id}")]
        public async Task<ActionResult> DeleteModes(int id)
        {
            var mode = await unitOfWork.BilliardsModeRepository.GetModeByIdAsync(id);
            if (mode == null) return BadRequest("Mode doesn't exist!");

            // -------------------------------------------------- add checker if it's already used in match record history
            unitOfWork.BilliardsModeRepository.DeleteMode(mode);
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Delete failed");
        }

        [AllowAnonymous]
        [Authorize]
        [HttpGet("get-tournament-modes/{tournamentId}")]
        public async Task<ActionResult<IEnumerable<TournamentModeDto>>> GetTournamentModes(int tournamentId)
        {
            return Ok(await unitOfWork.BilliardsModeRepository.GetTournamentModesAsync(tournamentId));
        }

        [AllowAnonymous]
        [Authorize]
        [HttpGet("get-tournament-modes-name/{tournamentId}")]
        public async Task<ActionResult<IEnumerable<TournamentModeDto>>> GetTournamentModesName(int tournamentId)
        {
            var query = await unitOfWork.BilliardsModeRepository.GetTournamentModesAsyncWithName(tournamentId);
            return Ok(query);
        }

        [HttpPost("insert-tournament-modes")]
        public async Task<ActionResult> InsertTournamentModes(TournamentModeDto dto)
        {
            var exist = await unitOfWork.BilliardsModeRepository.GetTournamentModeAsync(dto.TournamentId, dto.ModeId);
            if (exist != null) return BadRequest("Mode is already in tournament");

            var tour = await unitOfWork.BilliardsTournamentRepository.GetTournamentById(dto.TournamentId);
            if (tour == null) return BadRequest("Tournament is not valid!");

            var mode = await unitOfWork.BilliardsModeRepository.GetModeByIdAsync(dto.ModeId);
            if (mode == null) return BadRequest("Mode doesn't exist!");

            unitOfWork.BilliardsModeRepository.InsertTournamentMode(mapper.Map<TournamentMode>(dto));
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Insert failed");
        }

        [HttpDelete("delete-tournament-modes/{id}")]
        public async Task<ActionResult> DeleteTournamentModes(int id)
        {
            var modeExist = await unitOfWork.BilliardsModeRepository.GetTournamentModeAsync(id);
            if (modeExist == null) return BadRequest("Mode is not existing in current tournament");

            var tour = await unitOfWork.BilliardsTournamentRepository.GetTournamentById(modeExist.TournamentId);
            if (tour == null) return BadRequest("Tournament is not valid!");

            // -------------------------------------------------- add checker if it's already used in match record history

            unitOfWork.BilliardsModeRepository.DeleteTournamentMode(modeExist);
            if (await unitOfWork.Complete()) return NoContent();

            return BadRequest("Delete failed");
        }
    }
}