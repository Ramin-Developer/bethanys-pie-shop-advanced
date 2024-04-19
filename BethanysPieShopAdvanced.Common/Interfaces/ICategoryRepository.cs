namespace BethanysPieShop.Common.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync();

    Task<Category?> GetCategoryByIdAsync(int id);

    Task<Category?> FindCategoryByTypeAsync(CategoryType categoryType);

    Task<int> UpdateCategoryAsync(Category category);

    Task<int> DeleteCategoryAsync(int id);

    Task<int> GetCategoriesCountAsync();
}
