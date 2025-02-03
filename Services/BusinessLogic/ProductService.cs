using AutoMapper;
using ProductApi.Models.Domain;
using ProductApi.Repository;
using ProductApi.Services.DTOs;
using ProductApi.Services.Interfaces;


namespace ProductApi.Services.BusinessLogic
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(p => _mapper.Map<ProductDTO>(p));
        }
        
        public async Task<ProductDTO> GetByIdAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }
        

        public async Task<ProductDTO> CreateAsync(CreateProductDTO createDto)
        {
            var product = _mapper.Map<Product>(createDto);
            product = await _repository.CreateAsync(product);
            return _mapper.Map<ProductDTO>(product);
        }
        
        public async Task<ProductDTO> UpdateAsync(Guid id, UpdateProductDTO updateDto)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return null;

            // Update only the allowed properties.
            product.Name = updateDto.Name;
            product.Description = updateDto.Description;
            // Offers or other nested entities are not modified here.

            product = await _repository.UpdateAsync(product);
            return _mapper.Map<ProductDTO>(product);
        }

        
        public async Task DeleteAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product != null)
                await _repository.DeleteAsync(product);
        }
        
        // Apply coupon discount
        public async Task<ProductWithDiscountDTO> GetProductWithDiscountAsync(Guid productId, Guid supplierId, string couponCode)
        {
            var product = await _repository.GetByIdAsync(productId);
            if (product == null)
                throw new Exception("Product not found.");

            // Find the offer from the specified supplier.
            var offer = product.Offers.FirstOrDefault(o => o.SupplierId == supplierId);
            if (offer == null)
                throw new Exception("Supplier offer not found for this product.");

            decimal originalPrice = offer.Price;
            decimal discountedPrice = originalPrice;
            string message = "Coupon not applied.";

            // Look for the coupon with the given code.
            var coupon = offer.Coupons.FirstOrDefault(c => c.Code.Equals(couponCode, StringComparison.OrdinalIgnoreCase));
            if (coupon == null)
            {
                message = "Coupon not found";
            }
            else
            {
                if (coupon.ExpiryDate < DateTime.UtcNow) message = "Coupon expired";
                
                else
                {
                    if (coupon.DiscountType == DiscountType.Percentage)
                        discountedPrice = originalPrice - (originalPrice * coupon.DiscountValue / 100);
                    else if (coupon.DiscountType == DiscountType.CashOff)
                        discountedPrice = originalPrice - coupon.DiscountValue;

                    if (discountedPrice < 0)
                        discountedPrice = 0;
                    
                    message = "Coupon applied successfully.";
                }
            }

            return new ProductWithDiscountDTO
            {
                ProductId = product.Id,
                ProductName = product.Name,
                SupplierId = supplierId,
                SupplierName = offer.Supplier.Name,
                OriginalPrice = originalPrice,
                DiscountedPrice = discountedPrice,
                Message = message
            };
        }
    }
}
