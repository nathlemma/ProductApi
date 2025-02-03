namespace ProductApi.Services.DTOs
{
    public class CreateCouponDTO
    {
        public string Code { get; set; }
        public int DiscountType { get; set; } // 0 for Percentage, 1 for CashOff
        public decimal DiscountValue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Guid ProductSupplierId { get; set; }
    }
}