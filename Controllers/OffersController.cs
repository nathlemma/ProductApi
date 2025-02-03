// Controllers/OffersController.cs
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models.Domain;
using ProductApi.Data;
using ProductApi.Services.DTOs;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OffersController : ControllerBase
    {
        private readonly ProductApiDbContext _dbContext;
        //Did not create repository layer because it's a small controller
        public OffersController(ProductApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateOffer([FromBody] CreateOfferDTO offerDto)
        {
            var offer = new ProductSupplier
            {
                ProductId = offerDto.ProductId,
                SupplierId = offerDto.SupplierId,
                Price = offerDto.Price
            };
            _dbContext.ProductSuppliers.Add(offer);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOffer), new { id = offer.Id }, offer);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOffer(Guid id)
        {
            var offer = await _dbContext.ProductSuppliers.FindAsync(id);
            if (offer == null)
                return NotFound();
            return Ok(offer);
        }
    }
}