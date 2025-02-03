// Controllers/ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using ProductApi.Services.DTOs;
using ProductApi.Services.Interfaces;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductsController(IProductService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, [FromQuery] Guid? supplierId, [FromQuery] string couponCode=null)
        {
            
            if (supplierId.HasValue && !string.IsNullOrEmpty(couponCode))
            {
                    var result = await _service.GetProductWithDiscountAsync(id, supplierId.Value, couponCode);
                    return Ok(result);
            }
            else
            {
                var product = await _service.GetByIdAsync(id);
                return Ok(product);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDTO productDto)
        {
            var created = await _service.CreateAsync(productDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDTO updateDto)
        {
            var updated = await _service.UpdateAsync(id, updateDto);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
