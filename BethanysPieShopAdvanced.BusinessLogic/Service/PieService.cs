namespace BethanysPieShop.BusinessLogic.Service;

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
        var pie = await _pieRepo.GetPieByIdAsync(id);

        return _pieMapper
            .Map<PieDto?>(pie);
    }

    public async Task<PieDto?> GetPieByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(PieValues.NameInvalidError, nameof(name));

        var pie = await _pieRepo
            .GetPieByNameAsync(name);

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
            throw new ArgumentException(PieValues.NameInvalidError, nameof(pieUpdate));

        if (pieUpdate.CategoryId <= 0)
            throw new ArgumentException(PieValues.InvalidCategoryId, nameof(pieUpdate));

        var existingPie = await _pieRepo.GetPieByIdAsync(pieUpdate.Id);
        if (existingPie != null && existingPie.Id != pieUpdate.Id)
            throw new ArgumentException(PieValues.NameDuplicatedError, nameof(pieUpdate));

        var pieToUpdate = await _pieRepo
            .GetPieByIdAsync(pieUpdate.Id)
            ?? throw new ArgumentException(PieValues.UpdateTargetNullError, nameof(pieUpdate));

        if (string.IsNullOrWhiteSpace(pieUpdate.CategoryName) == false)
        {
            var category = await _catRepo
                .GetCategoryByNameAsync(pieUpdate.CategoryName)
                ?? throw new ArgumentException(CategoryValues.CategoryNotFoundError, nameof(pieUpdate));

            pieToUpdate.CategoryId = category.Id;
        }

        //if (pieUpdate.RowVersion != null)
        //    pieToUpdate.RowVersion = pieUpdate.RowVersion;

        // Todo: Update profile mapping from PieDto to Pie with regard to Timestamp/RowVersion. 
        _pieMapper.Map(pieUpdate, pieToUpdate);

        var rowsAffected = await _pieRepo.UpdatePieAsync(pieToUpdate);

        return rowsAffected;
    }

    public async Task<int> DeletePieAsync(int id) =>
        await _pieRepo.DeletePieAsync(id);

    private readonly IPieRepository _pieRepo = pieRepo
        ?? throw new ArgumentNullException(nameof(pieRepo), GeneralValues.ArgumentNullError);

    private readonly ICategoryRepository _catRepo = catRepo
        ?? throw new ArgumentNullException(nameof(pieRepo), GeneralValues.ArgumentNullError);

    private readonly IMapper _pieMapper = pieMapper
                ?? throw new ArgumentNullException(nameof(pieMapper), GeneralValues.ArgumentNullError);
}
