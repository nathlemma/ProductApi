// Data/ProductApiDbContext.cs
using Microsoft.EntityFrameworkCore;
using ProductApi.Models.Domain;

namespace ProductApi.Data
{
    public class ProductApiDbContext : DbContext
    {
        public ProductApiDbContext(DbContextOptions<ProductApiDbContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSupplier> ProductSuppliers { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product → ProductSupplier (one to many)
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Offers)
                .WithOne(o => o.Product)
                .HasForeignKey(o => o.ProductId);

            // Supplier 0 ProductSupplier (one to many)
            modelBuilder.Entity<Supplier>()
                .HasMany(s => s.Offers)
                .WithOne(o => o.Supplier)
                .HasForeignKey(o => o.SupplierId);

            // ProductSupplier → Coupon (one to many)
            modelBuilder.Entity<ProductSupplier>()
                .HasMany(ps => ps.Coupons)
                .WithOne(c => c.ProductSupplier)
                .HasForeignKey(c => c.ProductSupplierId);

  
            modelBuilder.Entity<ProductSupplier>()
                .Property(ps => ps.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Coupon>()
                .Property(c => c.DiscountValue)
                .HasPrecision(18, 2);
        }
    }
}