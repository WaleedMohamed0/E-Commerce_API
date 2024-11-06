using E_Commerce.Core.Models;

namespace E_Commerce.Core.DTOs
{
    public class CustomerBasketDTO
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        public string? PaymentIntentId { get; set; } = string.Empty;
        public int? DeliveryMethodId { get; set; }
        public string? ClientSecret { get; set; }

    }
}
