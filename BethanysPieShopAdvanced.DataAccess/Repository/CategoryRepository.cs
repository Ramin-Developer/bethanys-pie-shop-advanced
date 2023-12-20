namespace BethanysPieShop.DataAccess.Repository;

public class CategoryRepository(PieShopDbContext dbContext, IMemoryCache memoryCache)
    : ICategoryRepository
{
    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        if (_memoryCache.TryGetValue(CategoriesCacheName, out List<Category>? categories) == false)
        {
            categories = await _dbContext
                .Categories
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .ToListAsync();

            var cacheEntryOptions =
                new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(60));

            _memoryCache.Set(CategoriesCacheName, categories, cacheEntryOptions);
        }

        return categories
            ?? Enumerable.Empty<Category>();
    }

    public async Task<Category?> GetCategoryByIdAsync(int id) =>
        await _dbContext
            .Categories
            .AsNoTracking()
            .Include(c => c.Pies)
            .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<Category?> GetCategoryByNameAsync(string name) =>
        await _dbContext
            .Categories
            .AsNoTracking()
            .Include(c => c.Pies)
            .FirstOrDefaultAsync(c => c.Name == name);

    public async Task<int> AddCategoryAsync(Category category)
    {
        await _dbContext
            .Categories
            .AddAsync(category);

        _memoryCache.Remove(CategoriesCacheName);

        return await _dbContext.SaveChangesAsync();
    }

    public async Task<int> UpdateCategoryAsync(Category category)
    {
        var catToUpdate = await _dbContext
            .Categories
            .FirstOrDefaultAsync(cat => cat.Id == category.Id)
                ?? throw new ArgumentException(CategoryValues.UpdateTargetNullError, nameof(category));

        // The category exists, update it and save changes.
        catToUpdate.Name = category.Name;
        catToUpdate.Description = category.Description;

        _dbContext
            .Categories
            .Update(catToUpdate);

        _memoryCache.Remove(CategoriesCacheName);

        return await _dbContext.SaveChangesAsync();
    }

    public async Task<int> UpdateCategoryNamesAsync(List<Category> categories)
    {
        foreach (var cat in categories)
        {
            var catToUpdate = await _dbContext
                .Categories
                .FirstOrDefaultAsync(c => c.Id == cat.Id);

            if (catToUpdate != null)
            {
                catToUpdate.Name = cat.Name;

                _dbContext
                    .Categories
                    .Update(catToUpdate);
            }
        }

        _memoryCache.Remove(CategoriesCacheName);

        return await _dbContext.SaveChangesAsync();
    }

    public async Task<int> DeleteCategoryAsync(int id)
    {
        var catToDelete =
            await GetCategoryByIdAsync(id);

        // Mark category and delete it.
        _dbContext.Categories.Remove(catToDelete!);
        _memoryCache.Remove(CategoriesCacheName);

        return await _dbContext.SaveChangesAsync();
    }

    private string CategoriesCacheName { get; } = "CategoriesCache";

    private readonly PieShopDbContext _dbContext = dbContext
            ?? throw new ArgumentNullException(nameof(dbContext), GeneralValues.ArgumentNullError);

    private readonly IMemoryCache _memoryCache = memoryCache
            ?? throw new ArgumentNullException(nameof(memoryCache), GeneralValues.ArgumentNullError);
}
