using E_Commerce.Core.DTOs;
using E_Commerce.Service.Services.BrandsAndTypes;
using E_Commerce.Service.Services.Products;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsTypesController : ControllerBase
    {
        private readonly IBrandTypeService brandService;
        public BrandsTypesController(IBrandTypeService brandService)
        {
            this.brandService = brandService;
        }
        [HttpGet("Brands")]
        public async Task<IEnumerable<TypeBrandDTO>> GetAllBrands()
         => await brandService.GetAllBrands();
        [HttpGet("Brand/{id:int}")]

        public async Task<GeneralResponse> GetBrandById(int id)
        {
            var brand = await brandService.GetBrandById(id);
            return brand == null ? GeneralResponse.Failure("Brand not found") : GeneralResponse.Success(brand);
        }
        [HttpGet("Brands/{name}")]
        public async Task<GeneralResponse> GetBrandsByName(string name)
        {
            var brand = await brandService.GetBrandsByName(name);
            return brand == null ? GeneralResponse.Failure("Brand not found") : GeneralResponse.Success(brand);
        }
        [HttpPost("AddBrand")]
        public async Task<TypeBrandDTO?> AddBrand(AddTypeBrandDTO brand)
            => brand == null ? null : await brandService.AddBrand(brand);
        [HttpDelete("DeleteBrand/{id}")]
        public async Task<GeneralResponse> DeleteBrand(int id)
        {
            var brand = await brandService.GetBrandById(id);
            if (brand == null)
            {
                return GeneralResponse.Failure("Brand not found");
            }
            await brandService.DeleteBrand(id);
            return GeneralResponse.Success("Brand deleted successfully");
        }

        // Product Types

        [HttpGet("Types")]
        public async Task<IEnumerable<TypeBrandDTO>> GetAllTypes()
            => await brandService.GetAllTypes();
        [HttpGet("Type/{id}")]
        public async Task<GeneralResponse> GetTypeById(int id)
        {
            var type = await brandService.GetTypeById(id);
            return type == null ? GeneralResponse.Failure("Type not found") : GeneralResponse.Success(type);
        }
        [HttpGet("Type/{name:alpha}")]
        public async Task<GeneralResponse> GetTypeByName(string name)
        {
            var type = await brandService.GetTypesByName(name);
            return type == null ? GeneralResponse.Failure("Type not found") : GeneralResponse.Success(type);
        }
        [HttpDelete("DeleteType/{id}")]
        public async Task<GeneralResponse> DeleteType(int id)
        {
            var type = await brandService.GetTypeById(id);
            if (type == null)
            {
                return GeneralResponse.Failure("Type not found");
            }
            await brandService.DeleteType(id);
            return GeneralResponse.Success("Type deleted successfully");
        }
    }
}
