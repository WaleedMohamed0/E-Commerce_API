using AutoMapper;
using E_Commerce.Core.DTOs.Orders;
using E_Commerce.Core.Models.Order;

namespace E_Commerce_API.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile(IConfiguration configuration)
        {
            CreateMap<Order, OrderReturnDTO>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost, o => o.MapFrom(s => s.DeliveryMethod.Cost));
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductItemOrder.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ProductItemOrder.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => $"{configuration["baseUrl"]}{s.ProductItemOrder.PictureUrl}"));
        }
    }
}
