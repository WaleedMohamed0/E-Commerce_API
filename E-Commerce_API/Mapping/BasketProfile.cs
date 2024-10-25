using AutoMapper;
using E_Commerce.Core.DTOs;
using E_Commerce.Core.Models;

namespace E_Commerce_API.Mapping
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, CustomerBasketDTO>().ReverseMap();
        }
    }
}
