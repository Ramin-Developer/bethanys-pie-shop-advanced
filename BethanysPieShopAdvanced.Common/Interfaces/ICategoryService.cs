namespace BethanysPieShop.Common.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategoriesAsync();

    Task<CategoryDto?> GetCategoryByIdAsync(int id);

    Task<CategoryDto?> FindCategoryByTypeAsync(CategoryType categoryType);

    Task<int> AddCategoryAsync(CategoryDto category);

    Task<int> UpdateCategoryAsync(CategoryDto updatedCategory);

    Task<int> DeleteCategoryAsync(int id);
}