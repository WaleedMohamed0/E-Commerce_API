using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Core.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public string? DisplayName { get; set; }
    }
}
