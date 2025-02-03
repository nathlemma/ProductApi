using Microsoft.AspNetCore.Mvc;
using ProductApi.Services.DTOs;
using ProductApi.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var suppliers = await _supplierService.GetAllSuppliersAsync();
            return Ok(suppliers);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var supplier = await _supplierService.GetSupplierByIdAsync(id);
            if (supplier == null)
                return NotFound();
            return Ok(supplier);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SupplierDTO supplierDto)
        {
            var created = await _supplierService.CreateSupplierAsync(supplierDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SupplierDTO supplierDto)
        {
            var updated = await _supplierService.UpdateSupplierAsync(id, supplierDto);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _supplierService.DeleteSupplierAsync(id);
            return NoContent();
        }
    }
}