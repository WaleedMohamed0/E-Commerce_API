using E_Commerce.Core.DTOs;
using E_Commerce.Core.Models;
using E_Commerce.Core.Models.Order;
using E_Commerce.Repository.Data.Repos;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = E_Commerce.Core.Models.Product;

namespace E_Commerce.Service.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;

        public PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork,IConfiguration configuration)
        {
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }
        public async Task<CustomerBasketDTO> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
            var basket = await basketRepository.GetBasketAsync(basketId);
            if (basket == null) return null;

            var shippingPrice = 0m;
            // Calculate shipping (Delivery Price)
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await unitOfWork.genericRepository<DeliveryMethod,int>().GetAsync(basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Cost;
            }
            // Update basket items price
            if (basket.Items.Count()>0)
            {
                foreach(var item in basket.Items)
                {
                    var product = await unitOfWork.genericRepository<Product, int>().GetAsync(item.Id);
                    if (item.ProductPrice != product.Price)
                    {
                        item.ProductPrice = product.Price;
                    }
                }
            }
            var subTotal = basket.Items.Sum(i => i.Quantity * i.ProductPrice);
            var service = new PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                // Create
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(subTotal * 100 + shippingPrice * 100), // In cents
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                var paymentIntent = await service.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                // Update
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)(subTotal * 100 + shippingPrice * 100) // In cents
                };
                var paymentIntent = await service.UpdateAsync(basket.PaymentIntentId,options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }

            var basketDTO = await basketRepository.UpdateBasketAsync(basket);
            if (basketDTO == null) return null;
            return basketDTO;
        }
    }
}
