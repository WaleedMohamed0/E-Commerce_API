namespace E_Commerce.Core.Models
{
    public class Brand : BaseEntity<int>
    {
        public string Name { get; set; }
        public List<Product>? products { get; set; }
    }
}
