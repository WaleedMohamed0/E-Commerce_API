using E_Commerce.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Services.BrandsAndTypes
{
    public interface IBrandTypeService
    {
        Task<IEnumerable<TypeBrandDTO>> GetAllBrands();
        Task<TypeBrandDTO> GetBrandById(int id);
        Task<IEnumerable<TypeBrandDTO>> GetBrandsByName(string name);
        Task DeleteBrand(int id);
        Task<TypeBrandDTO> AddBrand(AddTypeBrandDTO brand);

        // Product Types
        Task<IEnumerable<TypeBrandDTO>> GetAllTypes();
        Task<TypeBrandDTO> GetTypeById(int id);
        Task<IEnumerable<TypeBrandDTO>> GetTypesByName(string name);
        Task DeleteType(int id);
        Task<TypeBrandDTO> AddType(AddTypeBrandDTO type);


    }
}
