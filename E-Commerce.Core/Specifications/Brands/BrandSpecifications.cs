using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications.Brands
{
    public class BrandSpecifications : BaseSpecifications<Brand, int>
    {
        public BrandSpecifications()
        {
        }
        public BrandSpecifications(int id) : base(p => p.Id== id)
        {
        }
        public BrandSpecifications(string name) : base(p => p.Name.ToLower().Contains(name.ToLower()))
        {
        }
    }
}
