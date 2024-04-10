namespace BethanysPieShop.SharedConfiguration.ProfileMappings;

public class PieProfileMapping : Profile
{
    public PieProfileMapping()
    {
        // Pie -> PieDto
        _ = CreateMap<Pie, PieDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
            .ForMember(dest => dest.LongDescription, opt => opt.MapFrom(src => src.LongDescription))
            .ForMember(dest => dest.AllergyInformation, opt => opt.MapFrom(src => src.AllergyInformation))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.ImageThumbnailUrl, opt => opt.MapFrom(src => src.ImageThumbnailUrl))
            .ForMember(dest => dest.IsPieOfTheWeek, opt => opt.MapFrom(src => src.IsPieOfTheWeek))
            .ForMember(dest => dest.InStock, opt => opt.MapFrom(src => src.InStock))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src =>
                CategoryHelper.GetCategoryName(src.CategoryId)));

        // PieDto -> Pie
        _ = CreateMap<PieDto, Pie>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.CategoryId, opts => opts.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.ShortDescription, opts => opts.MapFrom(src => src.ShortDescription))
            .ForMember(dest => dest.LongDescription, opts => opts.MapFrom(src => src.LongDescription))
            .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Price))
            .ForMember(dest => dest.AllergyInformation, opts => opts.MapFrom(src => src.AllergyInformation))
            .ForMember(dest => dest.ImageUrl, opts => opts.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.ImageThumbnailUrl, opts => opts.MapFrom(src => src.ImageThumbnailUrl))
            .ForMember(dest => dest.InStock, opts => opts.MapFrom(src => src.InStock))
            .ForMember(dest => dest.IsPieOfTheWeek, opts => opts.MapFrom(src => src.IsPieOfTheWeek))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name));

        // Todo: Here implement the logic for adding a time stamp to the PieObject
        //.ForMember(dest => dest.RowVersion, opts => opts.MapFrom(src => src.RowVersion))
    }
}
