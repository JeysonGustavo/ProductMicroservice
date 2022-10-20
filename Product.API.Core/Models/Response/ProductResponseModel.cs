namespace Product.API.Core.Models.Response
{
    public class ProductResponseModel
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public decimal Cost { get; set; }
        public CategoryResponseModel Category { get; set; } = new CategoryResponseModel();
    }
}