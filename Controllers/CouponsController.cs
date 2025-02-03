using Microsoft.AspNetCore.Mvc;
using ProductApi.Models.Domain;
using ProductApi.Data;
using ProductApi.Services.DTOs;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CouponsController : ControllerBase
    {
        //Did not create repository layer because it's a small controller
        private readonly ProductApiDbContext _dbContext;
        public CouponsController(ProductApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCoupon([FromBody] CreateCouponDTO couponDto)
        {
            var coupon = new Coupon
            {
                Code = couponDto.Code,
                DiscountType = (DiscountType)couponDto.DiscountType,
                DiscountValue = couponDto.DiscountValue,
                ExpiryDate = couponDto.ExpiryDate,
                ProductSupplierId = couponDto.ProductSupplierId
            };
            _dbContext.Coupons.Add(coupon);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCoupon), new { id = coupon.Id }, coupon);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoupon(Guid id)
        {
            var coupon = await _dbContext.Coupons.FindAsync(id);
            if (coupon == null)
                return NotFound();
            return Ok(coupon);
        }
    }
}