using ProductApi.Services.DTOs;

namespace ProductApi.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDTO>> GetAllSuppliersAsync();
        Task<SupplierDTO> GetSupplierByIdAsync(Guid id);
        Task<SupplierDTO> CreateSupplierAsync(SupplierDTO supplierDto);
        Task<SupplierDTO> UpdateSupplierAsync(Guid id, SupplierDTO supplierDto);
        Task DeleteSupplierAsync(Guid id);
    }
}