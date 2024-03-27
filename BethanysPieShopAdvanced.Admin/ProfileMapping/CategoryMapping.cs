namespace BethanysPieShop.Admin.ProfileMapping;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        _ = CreateMap<CategoryDto, DetailsCategoryDto>();
    }
}
