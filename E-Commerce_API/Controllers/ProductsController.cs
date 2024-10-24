using E_Commerce.Service.Services.Products;
using E_Commerce.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Core.DTOs.ProductDTOs;
using E_Commerce.Core.Specifications.Products;
using E_Commerce.Core.Helper;
using E_Commerce_API.Errors;
using E_Commerce.Core.Models;

namespace E_Commerce_API.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet()]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductSpecParams productSpec)
        {
            var products = await productService.GetAllProducts(productSpec);
            return products == null ? NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound, "No Product found")) : Ok(GeneralResponse.Success(products));
        }
        
        [HttpGet("GetProduct/{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await productService.GetProductById(id);
            return product == null ? NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound, "No Product found")) : Ok(GeneralResponse.Success(product));
        }

        [HttpGet("SearchProducts/{name}")]
        public async Task<IActionResult> SearchByName(string name)
        {
            var product = await productService.SearchByName(name);
            return product == null ? NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound, "No Product found")) : Ok(GeneralResponse.Success(product));
        }

        [HttpPost("AddProduct")]
        public async Task<ReadProductsDTO?> AddProduct(AddProductDTO product)
            => product == null ? null : await productService.AddProduct(product);

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productService.GetProductById(id);
            if (product == null)
            {
                return NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound, "No Product found"));
            }
            await productService.DeleteProduct(id);
            return Ok($"Product {product.Name} deleted successfully");
        }
    }
}