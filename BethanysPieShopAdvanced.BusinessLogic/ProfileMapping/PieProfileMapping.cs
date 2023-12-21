namespace BethanysPieShop.BusinessLogic.ProfileMapping;

public class PieProfileMapping : Profile
{
    public PieProfileMapping()
    {
        // Mapping from Pie to PieDto
        CreateMap<Pie, PieDto>()
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
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
            .ForMember(dest => dest.CategoryName, opts => opts.MapFrom(
                src => (src.Category != null) ? src.Category.Name : string.Empty));

        // Mapping from PieDto to Pie (Reverse Mapping)
        CreateMap<PieDto, Pie>()
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
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))

            // Todo: Here implement the logic for adding a time stamp to the PieObject
            //.ForMember(dest => dest.RowVersion, opts => opts.MapFrom(src => src.RowVersion))
            
            .AfterMap((src, dest) =>
            {
                if (string.IsNullOrEmpty(src.CategoryName) == false)
                {
                    if (dest.Category == null)
                    {
                        dest.Category = new Category();
                    }
                    dest.Category.Name = src.CategoryName;
                }
            });
    }
}
