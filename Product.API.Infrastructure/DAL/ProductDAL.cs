using Microsoft.EntityFrameworkCore;
using Product.API.Core.Infrastructure;
using Product.API.Core.Models.Domain;
using Product.API.Infrastructure.Data;

namespace Product.API.Infrastructure.DAL
{
    public class ProductDAL : IProductDAL
    {
        private readonly AppDbContext _context;

        public ProductDAL(AppDbContext context)
        {
            _context = context;
        }

        public void CreateProduct(ProductModel product)
        {
            if (product is null)
                throw new ArgumentNullException(nameof(product));

            _context.Products.Add(product);
        }

        public IEnumerable<ProductModel> GetAllProducts() => _context.Products.Include(x => x.Category).ToList().OrderBy(x => x.Id);

        public ProductModel? GetProductById(int id) => _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id.Equals(id));

        public bool SaveChanges() => _context.SaveChanges() > 0;
    }
}