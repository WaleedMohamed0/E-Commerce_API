using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications.ProductTypes
{
    public class ProductTypeSpecifications : BaseSpecifications<ProductType, int>
    {
        public ProductTypeSpecifications()
        {
        }
        public ProductTypeSpecifications(int id) : base(p => p.Id == id)
        {
        }
        public ProductTypeSpecifications(string name) : base(p => p.Name.ToLower().Contains(name.ToLower()))
        {
        }
    }
}
