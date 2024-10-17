using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Core.DTOs.ProductDTOs
{
    public class AddProductDTO
    {
        [Required, MinLength(3, ErrorMessage = "Product Name Must be More than 3 characters")]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string? PictureUrl { get; set; }
        [Required]
        public int? TypeId { get; set; }
        [Required]
        public int? BrandId { get; set; }

    }
}
