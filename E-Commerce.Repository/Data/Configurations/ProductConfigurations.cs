using E_Commerce.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace E_Commerce.Repository.Data.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //builder.HasOne(p=>p.Brand)
            //    .WithMany()
            //    .HasForeignKey(p => p.BrandId)
            //    .OnDelete(DeleteBehavior.SetNull);

            //builder.HasOne(p => p.Type)
            //    .WithMany()
            //    .HasForeignKey(p => p.TypeId)
            //    .OnDelete(DeleteBehavior.SetNull);
            //builder.Property(p => p.BrandId).IsRequired(false);
        }
    }
}
