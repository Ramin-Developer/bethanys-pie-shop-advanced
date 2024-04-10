namespace BethanysPieShop.SharedConfiguration.ProfileMappings;

public class CategoryProfileMapping : Profile
{
    public CategoryProfileMapping()
    {
        // Category -> CategoryDto
        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
            .ForMember(dest => dest.DateAdded, opts => opts.MapFrom(src => src.DateAdded))
            .ForMember(dest => dest.PieList, opts => opts.MapFrom(src => src.Pies));

        // CategoryDto -> Category
        CreateMap<CategoryDto, Category>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opts => opts.MapFrom(src => src.Description))
            .ForMember(dest => dest.DateAdded, opts => opts.MapFrom(src => src.DateAdded))
            .ForMember(dest => dest.Pies, opts => opts.MapFrom(src => src.PieList));

        // CategoryDto -> DetailsCategoryDto
        _ = CreateMap<CategoryDto, DetailsCategoryDto>();
    }
}
