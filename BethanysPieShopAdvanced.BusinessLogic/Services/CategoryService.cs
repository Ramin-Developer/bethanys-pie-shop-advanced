namespace BethanysPieShop.BusinessLogic.Services;

public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : ICategoryService
{
    public async Task<List<CategoryDto>> GetCategoriesAsync()
    {
        var cat = (await _categoryRepo
            .GetCategoriesAsync())
            .OrderBy(c => c.Id)
            .ToList();

        return _mapper
            .Map<List<CategoryDto>>(cat)
            ?? [];
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        if (id <= 0)
            throw new EntityPropertyFormatException<Category>("Id", id.ToString());

        var category = await _categoryRepo
            .GetCategoryByIdAsync(id);

        return category == null
            ? throw new EntityNotFoundException<Category>(id)
            : _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto?> FindCategoryByTypeAsync(CategoryType categoryType)
    {
        var category = await _categoryRepo
            .FindCategoryByTypeAsync(categoryType);

        return category == null
            ? throw new EntityNotFoundException<Category>(categoryType.ToString())
            : _mapper.Map<CategoryDto>(category);
    }

    public async Task<int> GetCategioryCountAsync() =>
        await _categoryRepo
        .GetCategoriesCountAsync();

    public async Task<int> UpdateCategoryAsync(CategoryDto categoryUpdate)
    {
        ValidateCategory(categoryUpdate);

        var isPerformable = await CanPerformOperation(CrudOperation.Update, categoryUpdate);
        if (isPerformable == false)
            throw new EntityDuplicationException<Category>(CategoryValues.NameDuplicatedError, nameof(categoryUpdate));

        var updatedCategory = _mapper.Map<Category>(categoryUpdate);

        return await _categoryRepo
            .UpdateCategoryAsync(updatedCategory);
    }

    public async Task<int> DeleteCategoryAsync(int id)
    {
        _ = await GetCategoryByIdAsync(id)
                ?? throw new EntityPropertyFormatException<Category>("Id", id.ToString());

        var isPerformable = await CanPerformOperation(CrudOperation.Delete, null!, id);
        if (isPerformable == false)
            throw new CategoryNotEmptyException(id);

        return await _categoryRepo
            .DeleteCategoryAsync(id);
    }

    private async Task<bool> CanPerformOperation(
        CrudOperation operation,
        CategoryDto categoryDto,
        int? id = null)
    {
        return operation switch
        {
            CrudOperation.Update => await CanUpdateCategory(categoryDto),
            CrudOperation.Delete =>
                id != null
                ? await CanDeleteCategory(id.Value)
               : throw new ArgumentNullException(nameof(id), CategoryValues.IdNullError),

            _ => throw new NotImplementedException()
        };
    }

    private async Task<bool> CanUpdateCategory(CategoryDto categoryDto)
    {
        var resultOk = Enum
            .TryParse(categoryDto.Name, ignoreCase: false, out CategoryType categoryType);

        var existingCategory = default(CategoryDto);
        if (resultOk)
            existingCategory = await FindCategoryByTypeAsync(categoryType);

        return existingCategory == null ||
               (existingCategory.Name == categoryDto.Name && existingCategory.Id == categoryDto.Id);
    }

    private async Task<bool> CanDeleteCategory(int id)
    {
        var existingCategory = await GetCategoryByIdAsync(id);

        return existingCategory != null &&
               (existingCategory.PieList?.All(p => p.CategoryId != id) ?? true);
    }

    private void ValidateCategory(CategoryDto categoryDto)
    {
        if (categoryDto == null)
            throw new ArgumentNullException(nameof(categoryDto), GeneralValues.ArgumentNullError);
    }

    private readonly ICategoryRepository _categoryRepo = categoryRepository
        ?? throw new ArgumentNullException(nameof(categoryRepository), GeneralValues.ArgumentNullError);

    private readonly IMapper _mapper = mapper
            ?? throw new ArgumentNullException(nameof(mapper), GeneralValues.ArgumentNullError);
}
