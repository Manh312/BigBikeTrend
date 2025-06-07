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
        }
    }
}