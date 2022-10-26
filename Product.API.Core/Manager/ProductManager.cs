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

        public async Task CreateProduct(ProductModel product) => await _productDAL.CreateProduct(product);
        public async Task<IEnumerable<ProductModel>> GetAllProducts() => await _productDAL.GetAllProducts();

        public async Task<ProductModel?> GetProductById(int id) => await _productDAL.GetProductById(id);

        public async Task<bool> SaveChanges() => await _productDAL.SaveChanges();
    }
}