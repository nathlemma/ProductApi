namespace ProductApi.Models.Domain
{
    public class ProductSupplier
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public decimal Price { get; set; }
        
        // Navigation Properties
        public ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
    }
}