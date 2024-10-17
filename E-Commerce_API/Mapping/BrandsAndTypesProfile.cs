using AutoMapper;
using E_Commerce.Core.DTOs;
using E_Commerce.Core.Models;

namespace E_Commerce_API.Mapping
{
    public class BrandsAndTypesProfile : Profile
    {
        public BrandsAndTypesProfile(IConfiguration configuration)
        {
            CreateMap<AddTypeBrandDTO, Brand>();
            CreateMap<Brand, TypeBrandDTO>();

            CreateMap<AddTypeBrandDTO, ProductType>();
            CreateMap<ProductType, TypeBrandDTO>();
        }
    }
}
