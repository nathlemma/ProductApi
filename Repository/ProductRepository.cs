using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Models.Domain;

namespace ProductApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductApiDbContext _context;
        public ProductRepository(ProductApiDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Offers)
                .ThenInclude(o => o.Coupons)
                .Include(p => p.Offers)
                .ThenInclude(o => o.Supplier)
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Offers)
                .ThenInclude(o => o.Coupons)
                .Include(p => p.Offers)
                .ThenInclude(o => o.Supplier)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}