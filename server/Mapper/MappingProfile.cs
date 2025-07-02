using AutoMapper;
using server.Dto;
using server.Entities;

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
                .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src =>
                    src.DiscountPercentage.HasValue ? (src.OriginalPrice * src.DiscountPercentage.Value / 100) : 0m))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // Bỏ qua null

            // Ánh xạ từ Product sang ProductDetailResponseDto, bao gồm ProductDetails
            CreateMap<Entities.Product, ProductDetailResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.OriginalPrice, opt => opt.MapFrom(src => src.OriginalPrice))
                .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.DiscountPercentage))
                .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src =>
                    src.DiscountPercentage.HasValue ? (src.OriginalPrice * src.DiscountPercentage.Value / 100) : 0m))
                .ForMember(dest => dest.NewPrice, opt => opt.MapFrom(src => src.NewPrice))
                .ForMember(dest => dest.IsOnDiscount, opt => opt.MapFrom(src => src.IsOnDiscount))
                .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.StockQuantity))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AverageRating))
                .ForMember(dest => dest.TotalReviews, opt => opt.MapFrom(src => src.TotalReviews))
                .ForMember(dest => dest.InStock, opt => opt.MapFrom(src => src.InStock))
                .ForMember(dest => dest.IsFeatured, opt => opt.MapFrom(src => src.IsFeatured))
                .ForMember(dest => dest.ProductCategory, opt => opt.MapFrom(src => src.ProductCategories))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.ProductDetails != null ? src.ProductDetails.ParsedDetails ?? new ProductDetailData() : new ProductDetailData()))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // Bỏ qua null
            CreateMap<WishListItem, WishListItemResDto>();
        }
    }
}