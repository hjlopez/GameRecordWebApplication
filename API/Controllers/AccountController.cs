using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;
        private readonly ITokenService tokenService;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IUnitOfWork unitOfWork;

        public AccountController(UserManager<AppUser> userManager, IMapper mapper, ITokenService tokenService,
                                    SignInManager<AppUser> signInManager, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            // check if user exists
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken!");

            var user = mapper.Map<AppUser>(registerDto);

            user.UserName = registerDto.Username.ToLower();

            // set email to sample@email.com
            user.Email = "sample@email.com";

            // create user and save to db
            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            // add to member role
            var roleResult = await userManager.AddToRoleAsync(user, "Member");
            if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

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
        }

        private async Task<bool> UserExists(string username)
        {
            return await userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await userManager.Users.Include(p => p.Photos).SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username!");

            // set email to sample@email.com if empty
            if (user.Email == "" || user.Email == null) user.Email = "sample@email.com";

            // sign in the user
            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized();

            // if there is no photo
            var url = "";
            if (user.Photos.Count == 0) url = "";
            else url = user.Photos.FirstOrDefault().Url;

            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Token = await tokenService.CreateToken(user),
                GamerTag = user.GamerTag,
                PlayMH = user.PlayMH,
                PlayDota = user.PlayDota,
                JoinBilliards = user.JoinBilliards,
                PhotoUrl = url
            };
        }

        [HttpGet("get-user/{userId}")]
        public async Task<ActionResult<UserDto>> GetUserById(int userId)
        {
            var user = await unitOfWork.UserRepository.GetUserByIdAsync(userId);

            var dto = new UserDto();
            dto = mapper.Map(user, dto);

            var photo = await unitOfWork.UserRepository.GetPhoto(userId);
            if (photo == null) dto.PhotoUrl = "";
            else dto.PhotoUrl = photo.Url;
            
            return Ok(dto);
        }

        [HttpGet("get-locale")]
        public async Task<ActionResult> GetLocale()
        {
            string baseUrl = "https://mhw-db.com/locations";

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return Ok(data);
                        }

                        return BadRequest("Error in getting data");
                    }
                }
            }
        }

    }
}