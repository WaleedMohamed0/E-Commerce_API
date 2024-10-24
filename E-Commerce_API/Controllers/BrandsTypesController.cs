using E_Commerce.Core.DTOs;
using E_Commerce.Core.Models;
using E_Commerce.Service.Services.BrandsAndTypes;
using E_Commerce.Service.Services.Products;
using E_Commerce_API.Errors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace E_Commerce_API.Controllers
{
    public class BrandsTypesController : BaseController
    {
        private readonly IBrandTypeService brandService;
        public BrandsTypesController(IBrandTypeService brandService)
        {
            this.brandService = brandService;
        }
        [HttpGet("Brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await brandService.GetAllBrands();
            return brands == null ? NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound)) : Ok(GeneralResponse.Success(brands));
        }
    
        [HttpGet("Brand/{id:int}")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            var brand = await brandService.GetBrandById(id);
            return brand == null ? NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound)) : Ok(GeneralResponse.Success(brand));
        }
        [HttpGet("SearchBrands/{name}")]
        public async Task<IActionResult> SearchBrands(string name)
        {
            var brand = await brandService.GetBrandsByName(name);
            return brand == null ? NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound)) : Ok(GeneralResponse.Success(brand));
        }
        [HttpPost("AddBrand")]
        public async Task<TypeBrandDTO?> AddBrand(AddTypeBrandDTO brand)
            => brand == null ? null : await brandService.AddBrand(brand);
        [HttpDelete("DeleteBrand/{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await brandService.GetBrandById(id);
            if (brand == null)
            {
                return NotFound(new ApiErrorsResponse(404));
            }
            await brandService.DeleteBrand(id);
            return Ok($"Brand {brand.Name} deleted successfully");
        }

        // Product Types

        [HttpGet("Types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var types = await brandService.GetAllTypes();
            return types == null ? NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound)) : Ok(GeneralResponse.Success(types));
        }
        [HttpGet("Type/{id}")]
        public async Task<IActionResult> GetTypeById(int id)
        {
            var type = await brandService.GetTypeById(id);
            return type == null ? NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound)) : Ok(GeneralResponse.Success(type));
        }
        [HttpGet("SearchTypes/{name:alpha}")]
        public async Task<IActionResult> SearchTypes(string name)
        {
            var type = await brandService.GetTypesByName(name);
            return type == null ? NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound)) : Ok(GeneralResponse.Success(type));
        }
        [HttpPost("AddType")]
        public async Task<TypeBrandDTO?> AddType(AddTypeBrandDTO type)
            => type == null ? null : await brandService.AddType(type);
        [HttpDelete("DeleteType/{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            var type = await brandService.GetTypeById(id);
            if (type == null)
            {
                return NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound));
            }
            await brandService.DeleteType(id);
            return Ok($"Type {type.Name} deleted successfully");
        }
    }
}
