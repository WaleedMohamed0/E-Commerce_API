using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications.Products
{
    public class ProductWithCountSpec: BaseSpecifications<Product, int>
    {
        public ProductWithCountSpec(ProductSpecParams productSpec)
            : base(p => (productSpec.BrandId == null || productSpec.BrandId == p.BrandId) 
            && (productSpec.TypeId == null || productSpec.TypeId == p.TypeId))
        {
        }

    }
}
