﻿using E_Commerce.Service.Services.Products;
using E_Commerce.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Core.DTOs.ProductDTOs;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet()]
        public async Task<IEnumerable<ReadProductsDTO>> GetAllProducts([FromQuery] string? sort)
            => await productService.GetAllProducts(sort);
        
        [HttpGet("GetProduct/{id:int}")]
        public async Task<GeneralResponse> GetProductById(int id)
        {
            var product = await productService.GetProductById(id);
            return product == null ? GeneralResponse.Failure("Product not found") : GeneralResponse.Success(product);
        }

        [HttpGet("GetProducts/{name}")]
        public async Task<GeneralResponse> GetProductsByName(string name)
        {
            var product = await productService.GetProductsByName(name);
            return product == null ? GeneralResponse.Failure("No Product found") : GeneralResponse.Success(product);
        }



        [HttpPost("AddProduct")]
        public async Task<ReadProductsDTO?> AddProduct(AddProductDTO product)
            => product == null ? null : await productService.AddProduct(product);

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<GeneralResponse> DeleteProduct(int id)
        {
            var product = await productService.GetProductById(id);
            if (product == null)
            {
                return GeneralResponse.Failure("Product not found");
            }
            await productService.DeleteProduct(id);
            return GeneralResponse.Success("Product deleted successfully");
        }
    }
}