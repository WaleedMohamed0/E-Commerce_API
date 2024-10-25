using E_Commerce.Core.DTOs.BrandTypeDTO;
using E_Commerce.Service.Services.BrandsAndTypes;
using E_Commerce_API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    public class BrandsTypesController : BaseController
    {
        private readonly IBrandTypeService brandService;
        public BrandsTypesController(IBrandTypeService brandService)
        {
            this.brandService = brandService;
        }
        [ProducesResponseType(typeof(IEnumerable<TypeBrandDTO>), StatusCodes.Status200OK)]
        [HttpGet("Brands")]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await brandService.GetAllBrands();
            return Ok(GeneralResponse.Success(brands, "Brands retrieved successfully"));
        }

        [ProducesResponseType(typeof(TypeBrandDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorsResponse), StatusCodes.Status404NotFound)]
        [HttpGet("Brand/{id:int}")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            var brand = await brandService.GetBrandById(id);
            return brand == null ? NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound)) 
                : Ok(GeneralResponse.Success(brand, "Brand retrieved successfully"));
        }
        [ProducesResponseType(typeof(TypeBrandDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorsResponse), StatusCodes.Status404NotFound)]
        [HttpGet("SearchBrands/{name}")]
        public async Task<IActionResult> SearchBrands(string name)
        {
            var brand = await brandService.GetBrandsByName(name);
            return brand == null ? NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound)) 
                : Ok(GeneralResponse.Success(brand, "Brand retrieved successfully"));
        }
        [ProducesResponseType(typeof(TypeBrandDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorsResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("AddBrand")]
        public async Task<ActionResult<TypeBrandDTO?>> AddBrand(AddTypeBrandDTO brand)
        {
            var newBrand = await brandService.AddBrand(brand);
            return Ok(GeneralResponse.Success(newBrand, "Brand added successfully"));
        }
        [ProducesResponseType(typeof(TypeBrandDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorsResponse), StatusCodes.Status404NotFound)]
        [HttpDelete("DeleteBrand/{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await brandService.GetBrandById(id);
            if (brand == null)
            {
                return NotFound(new ApiErrorsResponse(404));
            }
            await brandService.DeleteBrand(id);
            return Ok(GeneralResponse.Success($"Brand {brand.Name} deleted successfully"));
        }

        // Product Types

        [ProducesResponseType(typeof(IEnumerable<TypeBrandDTO>), StatusCodes.Status200OK)]
        [HttpGet("Types")]
        public async Task<IActionResult> GetAllTypes()
        {
            var types = await brandService.GetAllTypes();
            return Ok(GeneralResponse.Success(types,"ProductTypes retrieved successfully"));
        }
        [ProducesResponseType(typeof(TypeBrandDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorsResponse), StatusCodes.Status404NotFound)]
        [HttpGet("Type/{id}")]
        public async Task<IActionResult> GetTypeById(int id)
        {
            var type = await brandService.GetTypeById(id);
            return type == null ? NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound)) 
                : Ok(GeneralResponse.Success(type, "ProductType retrieved successfully"));
        }
        [ProducesResponseType(typeof(TypeBrandDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorsResponse), StatusCodes.Status404NotFound)]
        [HttpGet("SearchTypes/{name:alpha}")]
        public async Task<IActionResult> SearchTypes(string name)
        {
            var type = await brandService.GetTypesByName(name);
            return type == null ? NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound))
                : Ok(GeneralResponse.Success(type, "ProductType retrieved successfully"));
        }
        [ProducesResponseType(typeof(TypeBrandDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorsResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("AddType")]
        public async Task<ActionResult<TypeBrandDTO>?> AddType(AddTypeBrandDTO type)
        { 
            var newType = await brandService.AddType(type);
            return Ok(GeneralResponse.Success(newType, "ProductType added successfully"));
        }
        [ProducesResponseType(typeof(TypeBrandDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorsResponse), StatusCodes.Status404NotFound)]
        [HttpDelete("DeleteType/{id}")]
        public async Task<IActionResult> DeleteType(int id)
        {
            var type = await brandService.GetTypeById(id);
            if (type == null)
            {
                return NotFound(new ApiErrorsResponse(StatusCodes.Status404NotFound));
            }
            await brandService.DeleteType(id);
            return Ok(GeneralResponse.Success($"ProductType {type.Name} deleted successfully"));
        }
    }
}
