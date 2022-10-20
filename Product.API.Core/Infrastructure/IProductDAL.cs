using Product.API.Core.Models.Domain;

namespace Product.API.Core.Infrastructure
{
    public interface IProductDAL
    {
        bool SaveChanges();

        IEnumerable<ProductModel> GetAllProducts();

        ProductModel? GetProductById(int id);

        void CreateProduct(ProductModel product);
    }
}