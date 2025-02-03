using AutoMapper;
using ProductApi.Models.Domain;
using ProductApi.Services.DTOs;

namespace ProductApi.Services.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Mapping from Product to ProductDTO.
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Offers, opt => opt.MapFrom(src => src.Offers));

            // Mapping from ProductSupplier to ProductOfferDTO.
            CreateMap<ProductSupplier, ProductOfferDTO>()
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Supplier.Name))
                .ForMember(dest => dest.AgencyName, opt => opt.MapFrom(src => src.Supplier.AgencyName))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ReverseMap()
                .ForMember(dest => dest.Supplier, opt => opt.Ignore());

            // Mapping for creating a product.
            CreateMap<CreateProductDTO, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Offers, opt => opt.Ignore());

            // Mapping for updating a product.
            CreateMap<UpdateProductDTO, Product>()
                .ForMember(dest => dest.Offers, opt => opt.Ignore());

            // Mapping from Product to ProductDTO (reverse mapping for update if needed).
            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.Offers, opt => opt.Ignore());

            // Mapping for Supplier.
            CreateMap<Supplier, SupplierDTO>().ReverseMap();
        }
    }
}