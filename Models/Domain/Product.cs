namespace ProductApi.Models.Domain
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        // Navigation Properties
        public ICollection<ProductSupplier> Offers { get; set; } = new List<ProductSupplier>();
    }
}