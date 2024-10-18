using AutoMapper;
using E_Commerce.Repository.Data.Repos;
using E_Commerce.Core.Models;
using E_Commerce.Core.DTOs;
using E_Commerce.Core.Specifications.Products;
using E_Commerce.Core.Specifications.ProductTypes;
using E_Commerce.Core.Specifications.Brands;
using E_Commerce.Core.DTOs.ProductDTOs;
using E_Commerce.Core.Helper;

namespace E_Commerce.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ProductResponse<ReadProductsDTO>> GetAllProducts(ProductSpecParams productSpec)
        {
            var specs = new ProductSpecifications(productSpec);
            var products = await unitOfWork.genericRepository<Product, int>().GetAllWithSpecAsync(specs);
            var mappedProducts = mapper.Map<IEnumerable<ReadProductsDTO>>(products);
            var countSpecs = new ProductWithCountSpec(productSpec);
            var count = await unitOfWork.genericRepository<Product, int>().GetCountAsync(countSpecs);
            return new ProductResponse<ReadProductsDTO>(productSpec.Limit, productSpec.Page,count, mappedProducts);
        }

        public async Task<ReadProductsDTO> GetProductById(int id)
        {
            var specs = new ProductSpecifications(id);

            var product = await unitOfWork.genericRepository<Product, int>().GetByIdWithSpecAsync(specs);
            return mapper.Map<ReadProductsDTO>(product);
        }
        public async Task<IEnumerable<ReadProductsDTO>> GetProductsByName(string name)
        {
            var specs = new ProductSpecifications(name: name);
            var products = await unitOfWork.genericRepository<Product, int>().GetAllWithSpecAsync(specs);
            return mapper.Map<IEnumerable<ReadProductsDTO>>(products);
        }
        public async Task<ReadProductsDTO> AddProduct(AddProductDTO product)
        {
            var newProduct = mapper.Map<Product>(product);
            await unitOfWork.genericRepository<Product, int>().AddAsync(newProduct);
            await unitOfWork.SaveAsync();
            return mapper.Map<ReadProductsDTO>(newProduct);
        }


        public async Task<IEnumerable<TypeBrandDTO>> GetAllTypes()
        {
            var specs = new ProductTypeSpecifications();
            var types = await unitOfWork.genericRepository<ProductType, int>().GetAllWithSpecAsync(specs);
            return mapper.Map<IEnumerable<TypeBrandDTO>>(types);
        }
        public async Task<TypeBrandDTO> GetTypeById(int id)
        {
            var specs = new ProductTypeSpecifications(id);
            var type = await unitOfWork.genericRepository<ProductType, int>().GetByIdWithSpecAsync(specs);
            return mapper.Map<TypeBrandDTO>(type);
        }
        public async Task<TypeBrandDTO> GetTypeByName(string name)
        {
            var specs = new ProductTypeSpecifications(name);
            var type = await unitOfWork.genericRepository<ProductType, int>().GetByNameAsync(specs);
            return mapper.Map<TypeBrandDTO>(type);
        }

        public async Task DeleteProduct(int id)
        {
            var specs = new ProductSpecifications(id);
            var product = await unitOfWork.genericRepository<Product, int>().GetByIdWithSpecAsync(specs);
            unitOfWork.genericRepository<Product, int>().Delete(product);
            await unitOfWork.SaveAsync();
        }
  
        public async Task DeleteType(int id)
        {
            var specs = new ProductTypeSpecifications(id);
            var ProductType = await unitOfWork.genericRepository<ProductType, int>().GetByIdWithSpecAsync(specs);
            unitOfWork.genericRepository<ProductType, int>().Delete(ProductType);
            await unitOfWork.SaveAsync();
        }
    }
}
