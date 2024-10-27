using E_Commerce.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Repository
{
    public static class IdentitySeed
    {
        public static async Task SeedData(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Bob",
                        Email = "Bob@gmail.com",
                        UserName = "Bobo",
                        PhoneNumber = "123456789"
                    },
                    new AppUser
                    {
                        DisplayName = "Tom",
                        Email = "Tom@gmail.com",
                        UserName = "Tommy",
                        PhoneNumber = "123456789"
                    }
                };
                await userManager.CreateAsync(users[0], "Pa$$w0rd");
                await userManager.CreateAsync(users[1], "Pa$$w0rd");
            }
        }
    }
}
