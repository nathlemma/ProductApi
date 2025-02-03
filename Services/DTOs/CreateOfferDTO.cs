namespace ProductApi.Services.DTOs
{
    public class CreateOfferDTO
    {
        public Guid ProductId { get; set; }
        public Guid SupplierId { get; set; }
        public decimal Price { get; set; }
    }
}