namespace ProductApi.Services.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ProductOfferDTO> Offers { get; set; } = new List<ProductOfferDTO>();
    }
}