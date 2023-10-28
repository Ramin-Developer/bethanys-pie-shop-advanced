namespace BethanysPieShop.BusinessLogic.Service;

// Todo: Add other validation rules to the methods.
public class CategoryService : ICategoryService
{
    public CategoryService(ICategoryRepository catRepo, IMapper mapper)
    {
        _catRepo = catRepo
        ?? throw new ArgumentNullException(nameof(catRepo), GeneralValues.ArgumentNullError);

        _mapper = mapper
            ?? throw new ArgumentNullException(nameof(mapper), GeneralValues.ArgumentNullError);
    }

    public async Task<List<CategoryDto>> GetCategoriesAsync()
    {
        var cat = (await _catRepo
            .GetCategoriesAsync())
            .ToList();

        return _mapper
            .Map<List<CategoryDto>>(cat)
            ?? new List<CategoryDto>();
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException(CategoryValues.IdInvalidError, nameof(id));

        var cat = await _catRepo
            .GetCategoryByIdAsync(id);

        return _mapper
            .Map<CategoryDto>(cat);
    }

    public async Task<CategoryDto?> GetCategoryByNameAsync(string name)
    {
        var cat = await _catRepo
            .GetCategoryByNameAsync(name);

        return _mapper
            .Map<CategoryDto>(cat);
    }

    public async Task<int> AddCategoryAsync(CategoryDto catDto)
    {
        ValidateCategory(catDto);

        var isPerformable = await CanPerformOperation(CrudOperation.Create, catDto);
        if (isPerformable == false)
            throw new ArgumentException(CategoryValues.NameDuplicatedError, nameof(catDto));

        var cat = _mapper.Map<Category>(catDto);

        return await _catRepo
            .AddCategoryAsync(cat);
    }

    public async Task<int> UpdateCategoryAsync(CategoryDto updatedCatDto)
    {
        ValidateCategory(updatedCatDto);

        var isPerformable = await CanPerformOperation(CrudOperation.Update, updatedCatDto);
        if (isPerformable == false)
            throw new ArgumentException(CategoryValues.NameDuplicatedError, nameof(updatedCatDto));

        var cat = _mapper.Map<Category>(updatedCatDto);

        return await _catRepo
            .UpdateCategoryAsync(cat);
    }

    public async Task<int> DeleteCategoryAsync(int id)
    {
        _ = await GetCategoryByIdAsync(id)
            ?? throw new ArgumentException(CategoryValues.NonExistantCategoryIdError, nameof(id));

        var isPerformable = await CanPerformOperation(CrudOperation.Delete, null!, id);
        if (isPerformable == false)
            throw new Exception(CategoryValues.NonEmptyError);

        return await _catRepo
            .DeleteCategoryAsync(id);
    }

    private async Task<bool> CanPerformOperation(CrudOperation operation, CategoryDto catDto, int? id = null) =>
        operation switch
        {
            CrudOperation.Create => await CanCreateCategory(catDto),
            CrudOperation.Update => await CanUpdateCategory(catDto),
            CrudOperation.Delete =>
                id != null
                ? await CanDeleteCategory(id.Value)
               : throw new ArgumentNullException(nameof(id), CategoryValues.IdNullError),

            _ => throw new NotImplementedException()
        };

    private async Task<bool> CanCreateCategory(CategoryDto catDto)
    {
        var existingCategory = await GetCategoryByNameAsync(catDto.Name);

        return existingCategory == null;
    }

    private async Task<bool> CanUpdateCategory(CategoryDto catDto)
    {
        var existingCategory = await GetCategoryByNameAsync(catDto.Name);

        return existingCategory == null ||
               (existingCategory.Name == catDto.Name && existingCategory.Id == catDto.Id);
    }

    private async Task<bool> CanDeleteCategory(int id)
    {
        var existingCategory = await GetCategoryByIdAsync(id);

        return existingCategory != null &&
               (existingCategory.PieDtoList?.All(p => p.CategoryId != id) ?? true);
    }

    private void ValidateCategory(CategoryDto catDto)
    {
        if (catDto == null)
            throw new ArgumentNullException(nameof(catDto), GeneralValues.ArgumentNullError);
    }

    private readonly ICategoryRepository _catRepo;
    private readonly IMapper _mapper;
}
