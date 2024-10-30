using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Core.DTOs.User
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",ErrorMessage = "Invalid Email")]
        [DefaultValue("User@example.com")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Username is required")]
        [MinLength(5, ErrorMessage = "Username must be at least 5 characters")]
        [MaxLength(15, ErrorMessage = "Username must be at most 15 characters")]
        [DefaultValue("User123")]
        public string UserName { get; set; }
        [Phone]
        [DefaultValue("1234567890")]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters")]
        [MaxLength(15, ErrorMessage = "Password must be at most 15 characters")]
        [DefaultValue("Password@123")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{5,15}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character and must be between 5 and 15 characters")]
        public string Password { get; set; }
    }
}
