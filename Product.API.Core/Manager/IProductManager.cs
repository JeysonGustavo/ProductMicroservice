using Product.API.Core.Models.Domain;

namespace Product.API.Core.Manager
{
    public interface IProductManager
    {
        Task<bool> SaveChanges();

        Task<IEnumerable<ProductModel>> GetAllProducts();

        Task<ProductModel?> GetProductById(int id);

        Task CreateProduct(ProductModel product);
    }
}