using E_Commerce.Core.Models;
using E_Commerce.Core.Models.Order;
using E_Commerce.Core.Specifications.Orders;
using E_Commerce.Repository.Data.Repos;
using E_Commerce.Service.Services.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketRepository basketRepository;
        private readonly IPaymentService paymentService;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepository, IPaymentService paymentService)
        {
            this.unitOfWork = unitOfWork;
            this.basketRepository = basketRepository;
            this.paymentService = paymentService;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress)
        {
            var basket = await basketRepository.GetBasketAsync(basketId);
            if (basket == null) return null;

            var items = new List<OrderItem>();
            if(basket.Items.Count()>0)
            {
                foreach(var item in basket.Items)
                {
                    var productItem = await unitOfWork.genericRepository<Product,int>().GetAsync(item.Id);
                    var productItemOrdered = new ProductItemOrder(productItem.Id, productItem.Name, productItem.PictureUrl);
                    var orderItem = new OrderItem(productItemOrdered, productItem.Price, item.Quantity);
                    items.Add(orderItem);
                }
            }
            var deliveryMethodObj = await unitOfWork.genericRepository<DeliveryMethod, int>().GetAsync(deliveryMethod);
            if (deliveryMethodObj == null) return null;
            var subtotal = items.Sum(item => item.Price * item.Quantity);
            if(!string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var spec = new OrderSpecificationsWithPaymentIntent(basket.PaymentIntentId);
                var existingPaymentIntent = await unitOfWork.genericRepository<Order, int>().GetByIdWithSpecAsync(spec);
                unitOfWork.genericRepository<Order, int>().Delete(existingPaymentIntent);
            }
            var basketDTO = await paymentService.CreateOrUpdatePaymentIntent(basketId);
            if (basketDTO == null) return null;
            var order = new Order(buyerEmail, shippingAddress, deliveryMethodObj, items, subtotal, basketDTO.PaymentIntentId);
            await unitOfWork.genericRepository<Order, int>().AddAsync(order);
            var result = await unitOfWork.SaveAsync();
            if (result <= 0) return null;
            return order;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId, string buyerEmail)
        {
            var spec = new OrderSpecifications(orderId,buyerEmail);
            var order = await unitOfWork.genericRepository<Order, int>().GetByIdWithSpecAsync(spec);
            if (order == null) return null;
            return order;
        }

        public async Task<IEnumerable<Order>?> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderSpecifications(buyerEmail);
            var orders= await unitOfWork.genericRepository<Order, int>().GetAllWithSpecAsync(spec);
            if (orders == null) return null;
            return orders;
        }
    }
}
