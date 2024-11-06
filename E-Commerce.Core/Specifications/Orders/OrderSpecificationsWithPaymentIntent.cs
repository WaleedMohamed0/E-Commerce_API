using E_Commerce.Core.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications.Orders
{
    public class OrderSpecificationsWithPaymentIntent : BaseSpecifications<Order,int>
    {
        public OrderSpecificationsWithPaymentIntent(string paymentIntent) : base(o => o.PaymentIntentId == paymentIntent)
        {
            
        }
    }
}
