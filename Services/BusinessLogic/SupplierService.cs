using AutoMapper;
using ProductApi.Models.Domain;
using ProductApi.Repository;
using ProductApi.Services.DTOs;
using ProductApi.Services.Interfaces;

namespace ProductApi.Services.BusinessLogic
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public SupplierService(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDTO>> GetAllSuppliersAsync()
        {
            var suppliers = await _supplierRepository.GetAllAsync();
            return suppliers.Select(s => _mapper.Map<SupplierDTO>(s));
        }

        public async Task<SupplierDTO> GetSupplierByIdAsync(Guid id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            return _mapper.Map<SupplierDTO>(supplier);
        }

        public async Task<SupplierDTO> CreateSupplierAsync(SupplierDTO supplierDto)
        {
            // Map the DTO to the domain model.
            var supplier = _mapper.Map<Supplier>(supplierDto);
            supplier = await _supplierRepository.CreateAsync(supplier);
            return _mapper.Map<SupplierDTO>(supplier);
        }

        public async Task<SupplierDTO> UpdateSupplierAsync(Guid id, SupplierDTO supplierDto)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            if (supplier == null)
                return null;

            // Update properties.
            supplier.Name = supplierDto.Name;
            supplier.AgencyName = supplierDto.AgencyName;
            supplier = await _supplierRepository.UpdateAsync(supplier);
            return _mapper.Map<SupplierDTO>(supplier);
        }

        public async Task DeleteSupplierAsync(Guid id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            if (supplier != null)
            {
                await _supplierRepository.DeleteAsync(supplier);
            }
        }
    }
}