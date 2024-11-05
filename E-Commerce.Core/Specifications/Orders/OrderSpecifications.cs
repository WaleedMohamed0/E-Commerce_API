using E_Commerce.Core.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications.Orders
{
    public class OrderSpecifications : BaseSpecifications<Order,int>
    {
        // Returns the order with the specified id
        public OrderSpecifications(int orderId,string buyerEmail) 
            : base(o => o.Id == orderId && o.BuyerEmail == buyerEmail)
        {
            Includes.Add(o => o.OrderItems);
            Includes.Add(o => o.DeliveryMethod);
        }
        // Returns the orders with the specified buyer email
        public OrderSpecifications(string buyerEmail) : base(o => o.BuyerEmail == buyerEmail)
        {
            Includes.Add(o => o.OrderItems);
            Includes.Add(o => o.DeliveryMethod);
        }
    }
}
