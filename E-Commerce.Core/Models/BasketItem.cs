namespace E_Commerce.Core.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImage { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductBrand { get; set; }
        public string? ProductType { get; set; }
        public int Quantity { get; set; }
    }
}
