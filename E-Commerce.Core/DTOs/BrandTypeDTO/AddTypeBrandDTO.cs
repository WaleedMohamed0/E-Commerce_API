using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Core.DTOs.BrandTypeDTO
{
    public class AddTypeBrandDTO
    {
        [Required(ErrorMessage = "Name Field Is Required")]
        [MinLength(3, ErrorMessage = "Name Field Must Be At Least 3 Characters")]
        public string Name { get; set; }
    }
}
