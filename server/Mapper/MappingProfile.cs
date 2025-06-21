using AutoMapper; // Import AutoMapper namespace
using server.Dto; // Import namespace cho UserDto
using server.Entities; // Đã có

namespace server.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Ánh xạ từ User sang UserDto và ngược lại
            CreateMap<User, UserDto>();
            CreateMap<Image, ImageDtoRes>();
            CreateMap<CreateProductReq, Entities.Product>()
                .ForMember(x => x.Thumbnail, opt => opt.Ignore());
            CreateMap<CreateBrandReq, Brand>()
                .ForMember(x => x.Image, opt => opt.Ignore());
            CreateMap<CreateProductCategoriesReq, ProductCategories>()
                .ForMember(x => x.Image, opt => opt.Ignore());
            CreateMap<ProductCategories, ProductCategoriesResDto>();
            CreateMap<Brand, BrandResDto>();
            CreateMap<Entities.Product, ProductResDto>()
                .ForMember(dest => dest.ProductCategoriesResDto, opt => opt.MapFrom(src => src.ProductCategories))
                .ForMember(dest => dest.BrandResDto, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.DicountAmount, opt => opt.MapFrom(src =>
                src.DiscountPercentage.HasValue ? (src.OriginalPrice * src.DiscountPercentage.Value / 100) : 0m));
            CreateMap<ProductDetails, ProductDetailsResDto>()
            .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.ParsedDetails ?? new ProductDetailData()))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));
        }
    }
}