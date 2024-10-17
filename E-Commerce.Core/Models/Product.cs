﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Core.Models
{
    public class Product : BaseEntity<int>
    {
        [Required, MinLength(3, ErrorMessage = "Product Name Must be More than 3 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Product Description is Required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Product Price is Required")]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }
        public string? PictureUrl { get; set; }
        [Required(ErrorMessage = "Product Type is Required")]
        public int ?TypeId { get; set; }
        public ProductType? Type { get; set; }
        [Required(ErrorMessage = "Product Brand is Required")]
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }
    }
}