namespace E_Commerce.Core.Specifications.Products
{
    public class ProductSpecParams
    {
        public ProductSpecParams()
        {
            
        }
        public ProductSpecParams(string? sort, int? brandId, int? typeId, int limit, int page)
        {
            Sort = sort;
            BrandId = brandId;
            TypeId = typeId;
            Limit = limit;
            Page = page;
        }

        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public int Limit { get; set; } = 3;
        public int Page { get; set; } = 1;
    }
}
