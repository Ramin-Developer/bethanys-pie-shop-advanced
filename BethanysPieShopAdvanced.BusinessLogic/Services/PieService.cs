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
            throw new EntityPropertyFormatException<Pie>("Id", id.ToString());

        var pie = await _pieRepo
            .GetPieByIdAsync(id);

        return pie == null
            ? throw new EntityNotFoundException<Pie>(id)
            : _pieMapper.Map<PieDto>(pie);
    }

    public async Task<PieDto?> GetPieByNameAsync(string pieName)
    {
        if (string.IsNullOrWhiteSpace(pieName))
            throw new EntityPropertyFormatException<Pie>("Name", pieName);

        var pie = await _pieRepo
            .GetPieByNameAsync(pieName);

        return pie == null
            ? null
            : (_pieMapper
            ?.Map<PieDto?>(pie));
    }

    public async Task<int> GetPiesCountAsync() =>
        await _pieRepo.GetPiesCountAsync();

    public async Task<PieDto> AddPieAsync(PieDto pieDto)
    {
        ArgumentNullException.ThrowIfNull(pieDto, nameof(pieDto));

        if (string.IsNullOrWhiteSpace(pieDto.Name))
            throw new EntityPropertyFormatException<Pie>("Name", pieDto.Name);

        var existingPie = await _pieRepo
            .GetPieByNameAsync(pieDto.Name);

        if (existingPie != null)
            throw new EntityDuplicationException<Pie>(nameof(Pie.Name), pieDto.Name);

        var pie = _pieMapper.Map<Pie>(pieDto);
        _ = await _pieRepo.AddPieAsync(pie);

        return _pieMapper.Map<PieDto>(pie);
    }

    public async Task<int> UpdatePieAsync(int id, PieDto pieDto)
    {
        await ValidatePieUpdateAsync(id, pieDto);

        var existingPie = await _pieRepo
            .GetPieByIdAsync(pieDto.Id)
            ?? throw new EntityNotFoundException<Pie>(pieDto.Id);

        _pieMapper.Map(pieDto, existingPie);

        return await _pieRepo
            .UpdatePieAsync(existingPie!);
    }

    public async Task<int> DeletePieAsync(int id)
    {
        var pieExists = await PieExistsAsync(id);
        if (pieExists == false)
            throw new EntityNotFoundException<Pie>(id);

        return await _pieRepo.DeletePieAsync(id);
    }

    private async Task ValidatePieUpdateAsync(int id, PieDto pieUpdate)
    {
        ArgumentNullException.ThrowIfNull(pieUpdate, nameof(pieUpdate));

        if (id != pieUpdate.Id)
            throw new EntityIdMismatchException<Pie>(id);

        if (string.IsNullOrWhiteSpace(pieUpdate.Name))
            throw new EntityPropertyFormatException<Pie>("Name", pieUpdate.Name);

        if (pieUpdate.Id <= 0)
            throw new EntityPropertyFormatException<Pie>("Id", pieUpdate.Id.ToString());

        var pieExists = await PieExistsAsync(pieUpdate.Id);
        if (pieExists == false)
            throw new EntityNotFoundException<Pie>(pieUpdate.Id);

        if (pieUpdate.CategoryId <= 0)
            throw new EntityPropertyFormatException<Category>("Id", pieUpdate.CategoryId.ToString());

        if (pieUpdate.Price <= 0)
            throw new EntityPropertyFormatException<Pie>("Price", pieUpdate.Price.ToString());
    }

    private async Task<bool> PieExistsAsync(int id) =>
        await _pieRepo
            .GetPies()
            .AnyAsync(Pie => Pie.Id == id);

    private readonly IPieRepository _pieRepo = pieRepo
        ?? throw new ArgumentNullException(nameof(pieRepo), GeneralValues.ArgumentNullError);

    private readonly ICategoryRepository _categoryRepo = categoryRepo
        ?? throw new ArgumentNullException(nameof(pieRepo), GeneralValues.ArgumentNullError);

    private readonly IMapper _pieMapper = pieMapper
                ?? throw new ArgumentNullException(nameof(pieMapper), GeneralValues.ArgumentNullError);
}
