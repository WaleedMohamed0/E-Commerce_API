using E_Commerce.Core;
using E_Commerce.Core.DTOs.User;
using E_Commerce.Core.Models.Identity;
using E_Commerce.Service.Services.Tokens;
using E_Commerce.Service.Services.User;
using E_Commerce_API.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce_API.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService userService;
        private readonly UserManager<AppUser> userManager;
        private readonly ITokenService tokenService;

        public AccountController(IUserService userService,
            UserManager<AppUser> userManager,
            ITokenService tokenService)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.tokenService = tokenService;
        }
        [ProducesResponseType<LoginDTO>(StatusCodes.Status200OK)]
        [ProducesResponseType<LoginDTO>(StatusCodes.Status401Unauthorized)]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await userService.Login(loginDTO);
            if (user is null) return Unauthorized(new ApiErrorsResponse(StatusCodes.Status401Unauthorized,"Invalid Email or Password"));
            return Ok(GeneralResponse.Success(user, "User logged in successfully"));
        }
        [ProducesResponseType<RegisterDTO>(StatusCodes.Status200OK)]
        [ProducesResponseType<RegisterDTO>(StatusCodes.Status400BadRequest)]
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (await userService.CheckEmailExists(registerDTO.Email)) return BadRequest(new ApiErrorsResponse(StatusCodes.Status400BadRequest, "Email already exists"));

            var user = await userService.Register(registerDTO);
            if (user is null) return BadRequest(new ApiErrorsResponse(StatusCodes.Status400BadRequest));
            
            return Ok(GeneralResponse.Success(user, "User registered successfully"));
        }
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null) return BadRequest(new ApiErrorsResponse(StatusCodes.Status400BadRequest));
            var user = await userManager.FindByEmailAsync(userEmail);
            if (user is null) return BadRequest(new ApiErrorsResponse(StatusCodes.Status400BadRequest));
            var userDTO = new UserDTO()
            {
                UserName = user.UserName,
                Email = userEmail,
                PhoneNo = user.PhoneNumber,
                Token = await tokenService.CreateTokenAsync(user, userManager)
            };
            return Ok(GeneralResponse.Success(userDTO));
        }
    }
}
