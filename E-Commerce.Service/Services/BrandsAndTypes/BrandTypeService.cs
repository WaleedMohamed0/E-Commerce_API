using AutoMapper;
using E_Commerce.Core.DTOs;
using E_Commerce.Core.Models;
using E_Commerce.Core.Specifications.Brands;
using E_Commerce.Core.Specifications.ProductTypes;
using E_Commerce.Repository.Data.Repos;

namespace E_Commerce.Service.Services.BrandsAndTypes
{
    public class BrandTypeService : IBrandTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BrandTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<TypeBrandDTO>> GetAllBrands()
        {
            var specs = new BrandSpecifications();
            var brands = await unitOfWork.genericRepository<Brand, int>().GetAllWithSpecAsync(specs);
            // Map the brands to the DTO
            return mapper.Map<IEnumerable<TypeBrandDTO>>(brands);
        }

        public async Task<TypeBrandDTO> GetBrandById(int id)
        {
            var specs = new BrandSpecifications(id);
            var brand = await unitOfWork.genericRepository<Brand, int>().GetByIdWithSpecAsync(specs);
            return mapper.Map<TypeBrandDTO>(brand);
        }

        public async Task<IEnumerable<TypeBrandDTO>> GetBrandsByName(string name)
        {
            var specs = new BrandSpecifications(name);
            var brands = await unitOfWork.genericRepository<Brand, int>().GetAllWithSpecAsync(specs);
            return mapper.Map<IEnumerable<TypeBrandDTO>>(brands);
        }
        public async Task<TypeBrandDTO> AddBrand(AddTypeBrandDTO Brand)
        {
            var newBrand = mapper.Map<Brand>(Brand);
            await unitOfWork.genericRepository<Brand, int>().AddAsync(newBrand);
            await unitOfWork.SaveAsync();
            return mapper.Map<TypeBrandDTO>(newBrand);
        }
        public async Task DeleteBrand(int id)
        {
            var specs = new BrandSpecifications(id);
            var brand = await unitOfWork.genericRepository<Brand, int>().GetByIdWithSpecAsync(specs);
            unitOfWork.genericRepository<Brand, int>().Delete(brand);
            await unitOfWork.SaveAsync();
        }
        // Product Types
        public async Task<IEnumerable<TypeBrandDTO>> GetAllTypes()
        {
            var specs = new ProductTypeSpecifications();
            var types = await unitOfWork.genericRepository<ProductType, int>().GetAllWithSpecAsync(specs);
            return mapper.Map<IEnumerable<TypeBrandDTO>>(types);
        }

        public async Task<TypeBrandDTO> GetTypeById(int id)
        {
            var specs = new ProductTypeSpecifications(id);
            var types = await unitOfWork.genericRepository<ProductType, int>().GetByIdWithSpecAsync(specs);
            return mapper.Map<TypeBrandDTO>(types);
        }

        public async Task<IEnumerable<TypeBrandDTO>> GetTypesByName(string name)
        {
            var specs = new ProductTypeSpecifications(name);
            var types = await unitOfWork.genericRepository<ProductType, int>().GetAllWithSpecAsync(specs);
            return mapper.Map<IEnumerable<TypeBrandDTO>>(types);
        }
        public async Task<TypeBrandDTO> AddType(AddTypeBrandDTO type)
        {
            var newtype = mapper.Map<ProductType>(type);
            await unitOfWork.genericRepository<ProductType, int>().AddAsync(newtype);
            await unitOfWork.SaveAsync();
            return mapper.Map<TypeBrandDTO>(newtype);
        }
        public async Task DeleteType(int id)
        {
            var specs = new ProductTypeSpecifications(id);
            var type = await unitOfWork.genericRepository<ProductType, int>().GetByIdWithSpecAsync(specs);
            unitOfWork.genericRepository<ProductType, int>().Delete(type);
            await unitOfWork.SaveAsync();
        }
    }
}
