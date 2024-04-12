namespace BethanysPieShop.BusinessLogic.Services;

public class PieService(
    IPieRepository pieRepo,
    ICategoryRepository categoryRepo,
    IMapper pieMapper) : IPieService
{
    public async Task<List<PieDto>> GetPiesAsync()
    {
        var pies = await _pieRepo
            .GetPies()
            .OrderBy(p => p.Id)
            .ToListAsync();

        return _pieMapper
            .Map<List<PieDto>>(pies);
    }

    public async Task<List<PieDto>> GetPagedPiesAsync(RequestPage requestPage)
    {
        var pies = await _pieRepo
            .GetPagedPies(requestPage)
            .ToListAsync();

        return _pieMapper
            .Map<List<PieDto>>(pies);
    }

    public async Task<List<PieDto>> GetSortedPaginatedPiesAsync(
        RequestPage requestPage, PieSortOption sortOption)
    {
        var pies = await _pieRepo
            .GetSortedPaginatedPies(requestPage, sortOption)
            .ToListAsync();

        return _pieMapper
            .Map<List<PieDto>>(pies);
    }

    public async Task<List<PieDto>> SearchPiesAsync(int? categoryId, string searchQuery)
    {
        var pies = await _pieRepo
            .SearchPies(searchQuery, categoryId)
            .ToListAsync();

        return _pieMapper
            .Map<List<PieDto>>(pies);
    }

    public async Task<PieDto?> GetPieByIdAsync(int id)
    {
        if (id <= 0)
            throw new EntityIdFormatException<Pie>(id);

        var pie = await _pieRepo
            .GetPieByIdAsync(id);

        return pie == null

            ? throw new EntityNotFoundException<Pie>(id)
            : _pieMapper.Map<PieDto?>(pie);
    }

    public async Task<PieDto?> GetPieByNameAsync(string pieName)
    {
        if (string.IsNullOrWhiteSpace(pieName))
            throw new EntityNameFormatException<Pie>(pieName);

        var pie = await _pieRepo
            .GetPieByNameAsync(pieName);

        return pie == null
            ? null
            : (_pieMapper
            ?.Map<PieDto?>(pie));
    }

    public async Task<int> GetPiesCountAsync() =>
        await _pieRepo.GetNoOfPiesAsync();

    public async Task<int> AddPieAsync(PieDto pieDto)
    {
        if (pieDto == null)
            throw new ArgumentNullException(nameof(pieDto), GeneralValues.ArgumentNullError);

        if (string.IsNullOrWhiteSpace(pieDto.Name))
            throw new EntityNameFormatException<Pie>(pieDto.Name);

        var existingPie = await _pieRepo
            .GetPieByNameAsync(pieDto.Name);

        if (existingPie != null)
            throw new EntityDuplicationException<Pie>(nameof(Pie.Name), pieDto.Name);

        var pie = _pieMapper.Map<Pie>(pieDto);

        return await _pieRepo.AddPieAsync(pie);
    }

    public async Task<int> UpdatePieAsync(PieDto pieUpdate)
    {
        ValidatePieUpdate(pieUpdate);

        var existingPie = await _pieRepo
            .GetPieByIdAsync(pieUpdate.Id);

        if (existingPie != null && existingPie.Id != pieUpdate.Id)
            throw new EntityNotFoundException<Pie>(pieUpdate.Id);

        var pieToUpdate = await _pieRepo
            .GetPieByIdAsync(pieUpdate.Id)
            ?? throw new EntityNotFoundException<Pie>(pieUpdate.Id);

        await UpdateCategory(pieUpdate, pieToUpdate);

        _pieMapper.Map(pieUpdate, pieToUpdate);

        return await _pieRepo
            .UpdatePieAsync(pieToUpdate);
    }

    public async Task<int> DeletePieAsync(int id)
    {
        var pieExists = await PieExistsAsync(id);
        if (pieExists == false)
            throw new EntityNotFoundException<Pie>(id);

        return await _pieRepo.DeletePieAsync(id);
    }

    private void ValidatePieUpdate(PieDto pieUpdate)
    {
        // Check if the pieUpdate is null
        if (pieUpdate == null)
            throw new ArgumentNullException(nameof(pieUpdate), GeneralValues.ArgumentNullError);

        // Check if if pieUpdate.Name is null or empty
        if (string.IsNullOrWhiteSpace(pieUpdate.Name))
            throw new EntityNameFormatException<Pie>(pieUpdate.Name);

        // Check if pieUpdate.CategoryId is less than or equal to 0
        if (pieUpdate.CategoryId <= 0 || pieUpdate.CategoryId >= 4)
            throw new EntityIdFormatException<Category>(pieUpdate.CategoryId);
    }

    private async Task UpdateCategory(PieDto pieUpdate, Pie existingPie)
    {
        if (string.IsNullOrWhiteSpace(pieUpdate.CategoryName))
            throw new EntityNameFormatException<Category>(pieUpdate.CategoryName);

        if (Enum.TryParse<CategoryType>(pieUpdate.CategoryName, out var categoryType) == false)
            throw new EntityNameFormatException<Category>(pieUpdate.CategoryName);

        var category = await _categoryRepo
            .FindCategoryByTypeAsync(categoryType)
            ?? throw new EntityNotFoundException<Category>(pieUpdate.CategoryName);

        existingPie.CategoryId = category.Id;
    }

    private async Task<bool> PieExistsAsync(int id)
    {
        if (id <= 0)
            throw new EntityIdFormatException<Pie>(id);

        return await _pieRepo
            .GetPies()
            .AnyAsync(Pie => Pie.Id == id);
    }

    private readonly IPieRepository _pieRepo = pieRepo
        ?? throw new ArgumentNullException(nameof(pieRepo), GeneralValues.ArgumentNullError);

    private readonly ICategoryRepository _categoryRepo = categoryRepo
        ?? throw new ArgumentNullException(nameof(pieRepo), GeneralValues.ArgumentNullError);

    private readonly IMapper _pieMapper = pieMapper
                ?? throw new ArgumentNullException(nameof(pieMapper), GeneralValues.ArgumentNullError);
}
