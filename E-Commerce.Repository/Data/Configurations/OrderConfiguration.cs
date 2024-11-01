using E_Commerce.Core.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o=>o.Subtotal).HasColumnType("decimal(18,2)");
            // To make sure that the value of the enum is the same as the string value
            builder.Property(o=>o.Status)
                .HasConversion(o => o.ToString(),
                    o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o));
            // To make sure that OnDelete OrderItems Order should be deleted
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
            // To merge the DeliveryMethod with Order in Order table
            builder.OwnsOne(o => o.ShipToAddress, a => a.WithOwner());
        }
    }
}
