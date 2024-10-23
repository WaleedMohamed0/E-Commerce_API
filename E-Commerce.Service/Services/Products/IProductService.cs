using E_Commerce.Core.DTOs.ProductDTOs;
using E_Commerce.Core.Helper;
using E_Commerce.Core.Specifications.Products;

namespace E_Commerce.Service.Services.Products
{
    public interface IProductService
    {
        Task<ProductResponse<ReadProductsDTO>> GetAllProducts(ProductSpecParams productSpec);
        Task<ReadProductsDTO> GetProductById(int id);
        Task<IEnumerable<ReadProductsDTO>> SearchByName(string name);
        Task<ReadProductsDTO> AddProduct(AddProductDTO product);
        Task DeleteProduct(int id);
    }
}
