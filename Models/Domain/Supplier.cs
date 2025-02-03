namespace ProductApi.Models.Domain
{
    public class Supplier
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string AgencyName { get; set; } = string.Empty;
        // Navigation Properties
        public ICollection<ProductSupplier> Offers { get; set; } = new List<ProductSupplier>();
    }
}