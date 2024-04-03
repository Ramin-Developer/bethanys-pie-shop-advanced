namespace BethanysPieShop.Admin.ProfileMappings;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        // CategoryDto -> DetailsCategoryDto
        _ = CreateMap<CategoryDto, DetailsCategoryDto>();
    }
}
