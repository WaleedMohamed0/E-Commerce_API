using E_Commerce.Core.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Services.Orders
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress);
        Task<IEnumerable<Order>?> GetOrdersForUserAsync(string buyerEmail);
        Task<Order> GetOrderByIdAsync(int orderId, string buyerEmail);
    }
}
