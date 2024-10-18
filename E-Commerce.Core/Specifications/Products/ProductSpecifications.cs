using E_Commerce.Core.Models;

namespace E_Commerce.Core.Specifications.Products
{
    public class ProductSpecifications : BaseSpecifications<Product, int>
    {
        public ProductSpecifications()
        {
            AddIncludes();
        }
        public ProductSpecifications(int id) : base(p => p.Id == id)
        {
            AddIncludes();
        }
        public ProductSpecifications(string name) : base(p => p.Name.ToLower().Contains(name.ToLower()))
        {
            AddIncludes();
        }
        public ProductSpecifications(ProductSpecParams productSpec)
            : base(p => (productSpec.BrandId == null || productSpec.BrandId == p.BrandId) && (productSpec.TypeId == null || productSpec.TypeId == p.TypeId))
        {
            if(!string.IsNullOrEmpty(productSpec.Sort))
            {
                switch (productSpec.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
           
            AddIncludes();
            ApplyPagination(productSpec.Limit, productSpec.Page);
        }
        public void AddIncludes()
        {
            Includes.Add(p=> p.Brand);
            Includes.Add(p => p.Type);
        }
        public void ApplyPagination(int limit, int page)
        {
            IsPagingEnabled = true;
            Take = limit;
            Skip = (page - 1) * limit;
        }
    }
}
