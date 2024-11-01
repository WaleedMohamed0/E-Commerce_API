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
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // To merge the ProductItemOrder with OrderItem in OrderItem table
            builder.OwnsOne(i => i.ProductItemOrder, a => a.WithOwner());
            builder.Property(i => i.Price).HasColumnType("decimal(18,2)");


        }
    }
}
