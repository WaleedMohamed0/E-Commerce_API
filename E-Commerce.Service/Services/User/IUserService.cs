using E_Commerce.Core.DTOs.User;

namespace E_Commerce.Service.Services.User
{
    public interface IUserService
    {
        Task<UserDTO?> Login(LoginDTO loginDTO);
        Task<UserDTO?> Register(RegisterDTO registerDTO);
        Task<bool> CheckEmailExists(string email);
    }
}
