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
        public ProductSpecifications(string? sort = null, string? name = null)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Criteria = p => p.Name.ToLower().Contains(name.ToLower());
            }
            if(!string.IsNullOrEmpty(sort))
            {
                switch (sort)
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
        }
        public void AddIncludes()
        {
            Includes.Add(p=> p.Brand);
            Includes.Add(p => p.Type);
        }
    }
}
