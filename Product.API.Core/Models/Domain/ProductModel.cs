using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Product.API.Core.Models.Domain
{
    public class ProductModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        
        [Required]
        public string? ProductName { get; set; }
        
        [Required]
        public decimal Cost { get; set; }

        [Required]
        public int CategoryId { get; set; }
        
        public CategoryModel Category { get; set; }
    }
}