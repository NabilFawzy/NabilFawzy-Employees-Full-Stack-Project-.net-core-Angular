using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnionArch.Data.DTOs;
using OnionArch.Data.Entities.Identity;
using OnionArch.Data.Interfaces;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService tokenService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ITokenService tokenService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this.tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>>login(LoginDto loginDto)
        {
          
            var user = await _userManager.FindByNameAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized();
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            return new UserDto
            {
                Email = loginDto.Email,
                Token = tokenService.CreateToken(user),
                UserName = user.UserName
            };
        }


        [Authorize]
        [HttpGet("currentuser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user=await _userManager.FindByNameAsync(email);

            return new UserDto
            {
                Email = user.Email,
                Token = tokenService.CreateToken(user),
                UserName = user.UserName
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistAsync([FromQuery] string email)
        {
            bool t = await _userManager.FindByNameAsync(email) != null;
            return t;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _userManager.FindByNameAsync(registerDto.Email);
            if (user != null)
            {
                return Unauthorized();
            }
            // var result = await _signInManager.CheckPasswordSignInAsync(user, employee.Password, false);

             user = new AppUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,

                NormalizedEmail = registerDto.Email,
                NormalizedUserName = registerDto.Email,

            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            return new UserDto
            {
                Email = registerDto.Email,
                Token = tokenService.CreateToken(user),
                UserName = user.UserName
            };

        }

    }
}
