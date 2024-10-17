using E_Commerce.Core.DTOs;
using E_Commerce.Core.DTOs.ProductDTOs;

namespace E_Commerce.Service.Services.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ReadProductsDTO>> GetAllProducts(string? sort);
        Task<IEnumerable<TypeBrandDTO>> GetAllTypes();
        Task<ReadProductsDTO> GetProductById(int id);
        Task<IEnumerable<ReadProductsDTO>> GetProductsByName(string name);
        Task<ReadProductsDTO> AddProduct(AddProductDTO product);

        Task<TypeBrandDTO> GetTypeById(int id);
        Task<TypeBrandDTO> GetTypeByName(string name);
        Task DeleteProduct(int id);
        Task DeleteType(int id);

        //Task<ReadProductsDTO> UpdateProduct(int id, UpdateProductDTO product);

    }
}
