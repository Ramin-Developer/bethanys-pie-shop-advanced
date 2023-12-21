namespace BethanysPieShop.Admin.ProfileMapping;

// Todo: Move installation of AutoMapper and folder ProfileMapping to BethanysPieShop.WebApi.

// Todo: Include this code snippet at the end of CreateMap to add category name of the relevant pie to the mapping
//      .ForMember(dest => dest.Category.Name, opts => opts.MapFrom(
//          src => src.Pie!.CategoryName));
public class AdminProfileMapping : Profile
{
    public AdminProfileMapping()
    {
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

        //_ = CreateMap<Pie, PieDto>()
        //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        //    .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.ShortDescription))
        //    .ForMember(dest => dest.LongDescription, opt => opt.MapFrom(src => src.LongDescription))
        //    .ForMember(dest => dest.AllergyInformation, opt => opt.MapFrom(src => src.AllergyInformation))
        //    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
        //    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
        //    .ForMember(dest => dest.ImageThumbnailUrl, opt => opt.MapFrom(src => src.ImageThumbnailUrl))
        //    .ForMember(dest => dest.IsPieOfTheWeek, opt => opt.MapFrom(src => src.IsPieOfTheWeek))
        //    .ForMember(dest => dest.InStock, opt => opt.MapFrom(src => src.InStock))
        //    .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))       
        //    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.)       
    }
}
