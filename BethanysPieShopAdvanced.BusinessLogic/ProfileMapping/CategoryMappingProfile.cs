namespace BethanysPieShop.BusinessLogic.ProfileMapping;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        // Mapping from Category to CategoryDto
        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
            .ForMember(dest => dest.DateAdded, opts => opts.MapFrom(src => src.DateAdded))
            .ForMember(dest => dest.PieDtoList, opts => opts.MapFrom(src => src.Pies));

        // Mapping from CategoryDto to Category
        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
            .ForMember(dest => dest.DateAdded, opts => opts.MapFrom(src => src.DateAdded))
            .ForMember(dest => dest.Pies, opts => opts.MapFrom(src => src.PieDtoList));
    }
}
