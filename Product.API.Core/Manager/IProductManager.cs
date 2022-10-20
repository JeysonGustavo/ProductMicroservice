using Product.API.Core.Models.Domain;

namespace Product.API.Core.Manager
{
    public interface IProductManager
    {
        bool SaveChanges();

        IEnumerable<ProductModel> GetAllProducts();

        ProductModel? GetProductById(int id);

        void CreateProduct(ProductModel product);
    }
}