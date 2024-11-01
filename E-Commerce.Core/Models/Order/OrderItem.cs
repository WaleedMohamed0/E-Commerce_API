using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Models.Order
{
    public class OrderItem : BaseEntity<int>
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrder productItemOrder, decimal price, int quantity)
        {
            ProductItemOrder = productItemOrder;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrder ProductItemOrder { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
