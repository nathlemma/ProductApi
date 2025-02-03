namespace ProductApi.Models.Domain
{
    public enum DiscountType
    {
        Percentage,
        CashOff
    }

    public class Coupon
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = string.Empty;
        public DiscountType DiscountType { get; set; }
        //Percentage Coupon = 0, Cash off = 1
        public decimal DiscountValue { get; set; }
        public DateTime ExpiryDate { get; set; }
        
        //Navigation Properties - FK
        public Guid ProductSupplierId { get; set; }
        public ProductSupplier ProductSupplier { get; set; }
    }
}