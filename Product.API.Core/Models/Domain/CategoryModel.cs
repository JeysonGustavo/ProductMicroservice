using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Product.API.Core.Models.Domain
{
    public class CategoryModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string? CategoryName { get; set; }

        public List<ProductModel> Products { get; set; }
    }
}