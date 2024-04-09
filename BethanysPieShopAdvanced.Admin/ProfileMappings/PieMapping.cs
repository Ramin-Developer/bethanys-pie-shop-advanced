namespace BethanysPieShop.Admin.ProfileMappings;

// Todo: Move installation of AutoMapper and folder ProfileMapping to BethanysPieShop.WebApi.
public class PieMapping : Profile
{
    public PieMapping()
    {
        // PieAddViewModel -> PieDto
        _ = CreateMap<PieAddViewModel, PieDto>()
            .ForMember(dest => dest.ShortDescription, opts => opts.MapFrom(
                src => src.PieDto!.ShortDescription))

            .ForMember(dest => dest.LongDescription, opts => opts.MapFrom(
                src => src.PieDto!.LongDescription))

            .ForMember(dest => dest.Price, opts => opts.MapFrom(
                src => src.PieDto!.Price))

            .ForMember(dest => dest.AllergyInformation, opts => opts.MapFrom(
                src => src.PieDto!.AllergyInformation))

            .ForMember(dest => dest.ImageThumbnailUrl, opts => opts.MapFrom(
                src => src.PieDto!.ImageThumbnailUrl))

            .ForMember(dest => dest.ImageUrl, opts => opts.MapFrom(
                src => src.PieDto!.ImageUrl))

            .ForMember(dest => dest.InStock, opts => opts.MapFrom(
                src => src.PieDto!.InStock))

            .ForMember(dest => dest.IsPieOfTheWeek, opts => opts.MapFrom(
                src => src.PieDto!.IsPieOfTheWeek))

            .ForMember(dest => dest.Name, opts => opts.MapFrom(
                src => src.PieDto!.Name))

            // Todo: Check if this line shoulds be uncommented:
            //.ForMember(dest => dest.RowVersion, opts => opts.MapFrom(src => src.PieDto!.RowVersion));
            .ForMember(dest => dest.CategoryId, opts => opts.MapFrom(
                src => src.PieDto!.CategoryId));     
    }
}
