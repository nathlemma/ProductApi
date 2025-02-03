namespace ProductApi.Services.DTOs
{
    public class ProductOfferDTO
    {
        public Guid Id { get; set; }
        public string SupplierName { get; set; }
        public string AgencyName { get; set; }
        public decimal Price { get; set; }
    }
}