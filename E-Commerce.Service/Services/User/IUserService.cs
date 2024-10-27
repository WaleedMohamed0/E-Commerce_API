using E_Commerce.Core.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Services.User
{
    public interface IUserService
    {
        Task<UserDTO?> Login(LoginDTO loginDTO);
        Task<UserDTO?> Register(RegisterDTO registerDTO);
        Task<bool> CheckEmailExists(string email);
    }
}
