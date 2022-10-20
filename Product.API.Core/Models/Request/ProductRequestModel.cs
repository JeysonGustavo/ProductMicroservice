namespace Product.API.Core.Models.Request
{
    public class ProductRequestModel
    {
        public int CategoryId { get; set; }
        public string? ProductName { get; set; }
        public decimal Cost { get; set; }
    }
}