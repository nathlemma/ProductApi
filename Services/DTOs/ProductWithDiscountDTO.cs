namespace ProductApi.Services.DTOs
{
    public class ProductWithDiscountDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public string Message { get; set; }
    }
}