namespace BethanysPieShop.Common.Interface;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategoriesAsync();

    Task<CategoryDto?> GetCategoryByIdAsync(int id);

    Task<CategoryDto?> GetCategoryByNameAsync(string name);

    Task<int> AddCategoryAsync(CategoryDto category);

    Task<int> UpdateCategoryAsync(CategoryDto updatedCategory);

    Task<int> DeleteCategoryAsync(int id);
}