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

        public async Task CreateProduct(ProductModel product)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                if (product is null)
                    throw new ArgumentNullException(nameof(product));

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }

        public async Task<IEnumerable<ProductModel>> GetAllProducts() => await _context.Products.Include(x => x.Category).OrderBy(x => x.Id).ToListAsync();

        public async Task<ProductModel?> GetProductById(int id) => await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id.Equals(id));

        public async Task<bool> SaveChanges() => await _context.SaveChangesAsync() > 0;
    }
}