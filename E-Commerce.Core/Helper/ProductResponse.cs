using E_Commerce.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Helper
{
    public class ProductResponse<TEntity>
    {
        public ProductResponse(int limit, int page, int Count, IEnumerable<TEntity>? data)
        {
            // To Ensure that the limit is not greater than the total count
            this.limit = limit > Count ? Count : limit;
            this.page = page;
            this.Count = Count;
            Data = data;
        }

        public int limit { get; set; }
        public int page { get; set; }
        public int Count { get; set; }
        public IEnumerable<TEntity>? Data { get; set; }
    }
}
