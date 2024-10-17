using E_Commerce.Core.Models;
using E_Commerce.Repository.Data.Contexts;
using System.Text.Json;
namespace E_Commerce.Repository.Data
{
    public class LoadDataSeed
    {
        public static async Task SeedData(StoreDbContext context)
        {
            if (!context.Brands.Any())
            {
                var brandsData = File.ReadAllText("../E-Commerce.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<Brand>>(brandsData);
                if (brands is not null && brands.Count() > 0)
                {
                    await context.Brands.AddRangeAsync(brands);
                    await context.SaveChangesAsync();
                }
            }
            if (!context.Types.Any())
            {
                var typesData = File.ReadAllText("../E-Commerce.Repository/Data/DataSeed/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types is not null && types.Count() > 0)
                {
                    await context.Types.AddRangeAsync(types);
                    await context.SaveChangesAsync();
                }
            }
            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../E-Commerce.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products is not null && products.Count()>0)
                {
                    await context.Products.AddRangeAsync(products);
                    await context.SaveChangesAsync();
                }
            }
           
        }
    }
}
