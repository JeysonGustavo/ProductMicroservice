using Product.API.Core.Infrastructure;
using Product.API.Core.Models.Domain;

namespace Product.API.Core.Manager
{
    public class ProductManager : IProductManager
    {
        private readonly IProductDAL _productDAL;

        public ProductManager(IProductDAL productDAL)
        {
            _productDAL = productDAL;
        }

        public void CreateProduct(ProductModel product) => _productDAL.CreateProduct(product);
        public IEnumerable<ProductModel> GetAllProducts() => _productDAL.GetAllProducts();

        public ProductModel? GetProductById(int id) => _productDAL.GetProductById(id);

        public bool SaveChanges() => _productDAL.SaveChanges();
    }
}