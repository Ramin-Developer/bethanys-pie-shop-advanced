namespace BethanysPieShop.Admin.ProfileMappings;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        _ = CreateMap<CategoryDto, DetailsCategoryDto>();
    }
}
