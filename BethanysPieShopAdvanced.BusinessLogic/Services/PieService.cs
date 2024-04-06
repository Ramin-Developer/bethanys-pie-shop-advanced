namespace BethanysPieShop.BusinessLogic.Services;

public class PieService(
    IPieRepository pieRepo,
    ICategoryRepository catRepo,
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
            throw new InvalidEntityIdException("Pie", id);

        var pie = await _pieRepo
            .GetPieByIdAsync(id);

        return pie == null
            ? throw new EntityNotFoundException("Pie", id)
            : _pieMapper.Map<PieDto?>(pie);
    }

    public async Task<PieDto?> GetPieByNameAsync(string pieName)
    {
        if (string.IsNullOrWhiteSpace(pieName))
            throw new InvalidEntityNameException("Pie", pieName);

        var pie = await _pieRepo
            .GetPieByNameAsync(pieName);

        if (pie == null)
            throw new EntityNotFoundException("Pie", pieName);

        return _pieMapper
            ?.Map<PieDto?>(pie);
    }

    public async Task<int> GetPiesCountAsync() =>
        await _pieRepo.GetNoOfPiesAsync();

    public async Task<int> AddPieAsync(PieDto pieDto)
    {
        if (pieDto == null)
            throw new ArgumentNullException(nameof(pieDto), GeneralValues.ArgumentNullError);

        if (string.IsNullOrWhiteSpace(pieDto.Name))
            throw new ArgumentException(PieValues.NameInvalidError, nameof(pieDto));

        var existingPie = await _pieRepo.GetPieByNameAsync(pieDto.Name);
        if (existingPie != null)
            throw new ArgumentException(PieValues.NameDuplicatedError, nameof(pieDto));

        var pie = _pieMapper.Map<Pie>(pieDto);

        return await _pieRepo.AddPieAsync(pie);
    }

    public async Task<int> UpdatePieAsync(PieDto pieUpdate)
    {
        if (pieUpdate == null)
            throw new ArgumentNullException(nameof(pieUpdate), GeneralValues.ArgumentNullError);

        if (string.IsNullOrWhiteSpace(pieUpdate.Name))
            throw new InvalidEntityNameException("Pie", pieUpdate.Name);

        if (pieUpdate.CategoryId <= 0)
            throw new InvalidEntityIdException("Cayegory", pieUpdate.CategoryId);

        var existingPie = await _pieRepo.GetPieByIdAsync(pieUpdate.Id);
        if (existingPie != null && existingPie.Id != pieUpdate.Id)
            throw new EntityNotFoundException("Pie", pieUpdate.Name);

        var pieToUpdate = await _pieRepo
            .GetPieByIdAsync(pieUpdate.Id)
            ?? throw new EntityNotFoundException("Pie", pieUpdate.Id);

        if (string.IsNullOrWhiteSpace(pieUpdate.CategoryName) == false)
        {
            var category = await _catRepo
                .GetCategoryByNameAsync(pieUpdate.CategoryName)
                ?? throw new ArgumentException(CategoryValues.CategoryNotFoundError, nameof(pieUpdate));

            pieToUpdate.CategoryId = category.Id;
        }

        _pieMapper.Map(pieUpdate, pieToUpdate);

        var rowsAffected = await _pieRepo.UpdatePieAsync(pieToUpdate);

        return rowsAffected;
    }

    public async Task<int> DeletePieAsync(int id)
    {
        var pieExists = await PieExistsAsync(id);
        if (pieExists == false)
            throw new EntityNotFoundException("Pie", id);

        return await _pieRepo.DeletePieAsync(id);
    }

    private async Task<bool> PieExistsAsync(int id)
    {
        if (id <= 0)
            throw new InvalidEntityIdException("Pie", id);

        return await _pieRepo
            .GetPies()
            .AnyAsync(Pie => Pie.Id == id);
    }

    private readonly IPieRepository _pieRepo = pieRepo
        ?? throw new ArgumentNullException(nameof(pieRepo), GeneralValues.ArgumentNullError);

    private readonly ICategoryRepository _catRepo = catRepo
        ?? throw new ArgumentNullException(nameof(pieRepo), GeneralValues.ArgumentNullError);

    private readonly IMapper _pieMapper = pieMapper
                ?? throw new ArgumentNullException(nameof(pieMapper), GeneralValues.ArgumentNullError);
}
