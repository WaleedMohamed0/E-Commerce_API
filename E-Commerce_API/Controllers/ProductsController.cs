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
        [ProducesResponseType(typeof(ProductResponse<ReadProductsDTO>),StatusCodes.Status200OK)]
        [HttpGet()]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductSpecParams productSpec)
        {
            var products = await productService.GetAllProducts(productSpec);
            return products == null ? NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound, "No Product found")) : Ok(GeneralResponse.Success(products));
        }
        [ProducesResponseType(typeof(ProductResponse<ReadProductsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorsResponse),StatusCodes.Status404NotFound)]
        [HttpGet("GetProduct/{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await productService.GetProductById(id);
            return product == null ? NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound, "No Product found")) : Ok(GeneralResponse.Success(product));
        }

        [ProducesResponseType(typeof(ProductResponse<ReadProductsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorsResponse), StatusCodes.Status404NotFound)]
        [HttpGet("SearchProducts/{name}")]
        public async Task<IActionResult> SearchByName(string name)
        {
            var product = await productService.SearchByName(name);
            return product == null ? NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound, "No Product found")) : Ok(GeneralResponse.Success(product));
        }

        [ProducesResponseType(typeof(ReadProductsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorsResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("AddProduct")]
        public async Task<ActionResult<ReadProductsDTO>?> AddProduct(AddProductDTO product)
        {
            var newProduct = await productService.AddProduct(product);
            return Ok(GeneralResponse.Success(newProduct));
        }
        

        [ProducesResponseType(typeof(ReadProductsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorsResponse), StatusCodes.Status404NotFound)]
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