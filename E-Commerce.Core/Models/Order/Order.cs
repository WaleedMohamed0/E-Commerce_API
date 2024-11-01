using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Models.Order
{
    public class Order : BaseEntity<int>
    {
        public Order()
        {

        }
        public Order(string buyerEmail, Address shipToAddress, DeliveryMethod deliveryMethod, decimal subtotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public Address ShipToAddress { get; set; }
        public int DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public decimal Subtotal { get; set; }
        public string PaymentIntentId { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        //public string PaymentMethod { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public decimal GetTotal() => Subtotal + DeliveryMethod.Cost;
        
    }
}
