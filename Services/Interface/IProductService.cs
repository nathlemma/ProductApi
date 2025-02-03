using ProductApi.Services.DTOs;

namespace ProductApi.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetByIdAsync(Guid id);
        Task<ProductDTO> CreateAsync(CreateProductDTO createDto);
        Task<ProductDTO> UpdateAsync(Guid id, UpdateProductDTO updateDto);
        Task DeleteAsync(Guid id);

        // Apply a coupon discount to a supplier’s offer.
        Task<ProductWithDiscountDTO> GetProductWithDiscountAsync(Guid productId, Guid supplierId, string couponCode);
    }
}