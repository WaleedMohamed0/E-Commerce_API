﻿using AutoMapper;
using E_Commerce.Core.Models;
using E_Commerce.Core.DTOs;
using E_Commerce.Core.DTOs.ProductDTOs;

namespace E_Commerce.Core.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<Product, ReadProductsDTO>()
                .ForMember(dest => dest.BrandName, options => options.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.TypeName, options => options.MapFrom(src => src.Type.Name))
                // To map the PictureUrl to the full path
                .ForMember(dest=>dest.PictureUrl, options=>options.MapFrom(src => $"{configuration["baseUrl"]}{src.PictureUrl}"));
            CreateMap<ProductType,TypeBrandDTO>();
            CreateMap<AddTypeBrandDTO, Brand>();

            CreateMap<Brand, TypeBrandDTO > ();
            // To Ignore the Brand and Type properties in the AddProductDTO
            CreateMap<AddProductDTO, Product>()
                .ForMember(dest => dest.Brand, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.Ignore());

        }
    }
}