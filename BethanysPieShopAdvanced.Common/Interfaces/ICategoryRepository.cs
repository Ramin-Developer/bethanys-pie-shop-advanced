namespace BethanysPieShop.Common.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync();

    Task<Category?> GetCategoryByIdAsync(int id);

    Task<Category?> FindCategoryByTypeAsync(CategoryType categoryType);

    Task<int> AddCategoryAsync(Category category);

    Task<int> UpdateCategoryAsync(Category category);

    Task<int> UpdateCategoryNamesAsync(List<Category> categories);

    Task<int> DeleteCategoryAsync(int id);
}
