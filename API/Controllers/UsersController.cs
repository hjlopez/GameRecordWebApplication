using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly ITokenService tokenService;
        private readonly UserManager<AppUser> userManager;
        private readonly IPhotoService photoService;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService,
                               UserManager<AppUser> userManager, IPhotoService photoService)
        {
            this.photoService = photoService;
            //this.passwordStore = passwordStore;
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await unitOfWork.UserRepository.GetUsersAsync();
            var userDto = mapper.Map<IEnumerable<UserDto>>(users);

            return Ok(userDto);
        }

        [HttpPut]
        public async Task<ActionResult<UserDto>> UpdateUser(UserUpdateDto userUpdateDto)
        {
            var userById = await unitOfWork.UserRepository.GetUserByIdAsync(userUpdateDto.Id);
            var user = await unitOfWork.UserRepository.GetUserByUsernameAsync(userById.UserName);

            mapper.Map(userUpdateDto, user);
            unitOfWork.UserRepository.Update(user);
            if (await unitOfWork.Complete())
            {
                return new UserDto
                {
                    Id = user.Id,
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

        [HttpPost("change")]
        public async Task<ActionResult<string>> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = await unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            if (user == null) return Unauthorized();

            var result = await userManager.ChangePasswordAsync(user, changePasswordDto.oldPassword, changePasswordDto.NewPassword);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(new { message = "Password changed successfully!" });
        }

        [AllowAnonymous]
        [HttpPost("reset")]
        public async Task<ActionResult<AppUser>> ResetPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await unitOfWork.UserRepository.GetUserByUsernameAndTagAsync(forgotPasswordDto);
            // if no such user, return error
            if (user == null) return BadRequest("No user found!");
            // if no email, return message to display email field
            if (user.Email == "" || user.Email == null) return Ok(new { message = "No email registered." });

            // send temporary password to email
            // EmailSettings.SendEmailAsync(user.Email, user.GamerTag);

            return NoContent();
        }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            // check if user has existing photo
            if (user.Photos.Count != 0)
            {
                var photo = user.Photos.FirstOrDefault();
                var deleteResult = await photoService.DeletePhotoAsync(photo.PublicId);
                if (deleteResult.Error != null) return BadRequest(deleteResult.Error.Message);

                user.Photos.Remove(photo);
                unitOfWork.UserRepository.DeletePhoto(photo);
                if (!await unitOfWork.Complete()) return BadRequest("Failed to delete previous photo!");
            }

            // if no photo 
            var addResult = await photoService.AddPhotoAsync(file);
            if (addResult.Error != null) return BadRequest(addResult.Error.Message);

            var addPhoto = new Photo
            {
                Url = addResult.SecureUrl.AbsoluteUri,
                PublicId = addResult.PublicId
            };

            user.Photos.Add(addPhoto);
            var photoDto = new PhotoDto();
            if (await unitOfWork.Complete()) return Ok(mapper.Map(addPhoto, photoDto));

            return BadRequest();
        }

        [HttpDelete("delete-photo")]
        public async Task<ActionResult> DeletePhoto()
        {
            var user = await unitOfWork.UserRepository.GetUserByUsernameAsync(User.GetUsername());

            unitOfWork.UserRepository.DeletePhoto(user.Photos.FirstOrDefault());
            if (await unitOfWork.Complete()) return Ok(new {message = "Photo deleted!"});

            return BadRequest(new {message = "Cannot delete photo at the moment..."});
        }

    
    }
}