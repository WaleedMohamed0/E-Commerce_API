using E_Commerce.Core.DTOs.User;
using E_Commerce.Core.Models.Identity;
using E_Commerce.Service.Services.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Service.Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService tokenService;

        public UserService(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager,
            ITokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }
        public async Task<UserDTO?> Login(LoginDTO loginDTO)
        {
            var user = await userManager.FindByEmailAsync(loginDTO.Email);
            if(user is null) return null;
            var result = await signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (!result.Succeeded) return null;
            return new UserDTO() {
                Email = user.Email,
                UserName = user.UserName,
                PhoneNo = user.PhoneNumber,
                Token = await tokenService.CreateTokenAsync(user, userManager)
            };
        }

        public async Task<UserDTO?> Register(RegisterDTO registerDTO)
        {
            var user = new AppUser()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.UserName,
                PhoneNumber = registerDTO.PhoneNo
            };
            var result = await userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded) return null;
            return new UserDTO()
            {
                Email = user.Email,
                UserName = user.UserName,
                PhoneNo = user.PhoneNumber,
                Token = await tokenService.CreateTokenAsync(user, userManager)
            };
        }
        public async Task<bool> CheckEmailExists(string email)
            => await userManager.Users.AnyAsync(u => u.Email == email);
    }
}
