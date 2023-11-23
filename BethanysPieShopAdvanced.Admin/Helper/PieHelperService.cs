namespace BethanysPieShop.Admin.Helper;

public class PieHelperService(ICategoryService catService, IPieService pieService) : IPieHelperService
{
    public IEnumerable<SelectListItem> GetSelectList(IEnumerable<CategoryDto> catDTOs, int? selectedId) =>
        catDTOs.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name,
            Selected = c.Id == selectedId.GetValueOrDefault()
        })
        .ToList();

    public async Task PopulateCategorySelectListAsync(PieAddViewModel pieAddViewModel)
    {
        var catDTOs = await _catService.GetCategoriesAsync();
        pieAddViewModel.Categories =
            new SelectList(
                catDTOs, nameof(Category.Id), nameof(Category.Name), catDTOs.FirstOrDefault());
    }

    public async Task<PieSearchViewModel> CreatePieSearchViewModelAsync(string? searchQuery, int? searchCategory)
    {
        var catDTOs = await _catService
            .GetCategoriesAsync();

        var selectListItems = GetSelectList(catDTOs, null);

        if (searchQuery != null)
        {
            var pies = await _pieService
                .SearchPiesAsync(searchCategory, searchQuery);

            return new PieSearchViewModel()
            {
                Pies = pies,
                SearchCategory = searchCategory,
                Categories = selectListItems,
                SearchQuery = searchQuery
            };
        }

        return new PieSearchViewModel()
        {
            Pies = new List<PieDto>(),
            SearchCategory = null,
            Categories = selectListItems,
            SearchQuery = string.Empty
        };
    }

    public async Task<PieEditViewModel> CreatePieEditViewModelAsync(
        PieDto? pieToUpdate, int? selectedId = null!, string errorMessage = null!) =>
            new PieEditViewModel
            {
                PieDto = pieToUpdate,
                Categories = (await GetCategorySelectListAsync(selectedId)).ToList(),
                ErrorMessage = errorMessage
            };

    public async Task<PieAddViewModel> CreatePieAddViewModelAsync()
    {
        var selectCatDTOs = await GetCategorySelectListAsync();

        return new PieAddViewModel { Categories = selectCatDTOs };
    }

    public async Task<PaginatedPieViewModel> CreatePaginatedSortedViewModel(
        int? pageNumber, PieGroupOption groupOption, PieSortOption sortOption)
    {
        return groupOption switch
        {
            PieGroupOption.Paging => await CreatePaginatedViewModel(pageNumber),
            PieGroupOption.PagingSorting => await CreatePaginatedSortedViewModel(pageNumber, sortOption),
            _ => throw new ArgumentOutOfRangeException(nameof(groupOption)),
        };
    }

    private async Task<PaginatedPieViewModel> CreatePaginatedViewModel(int? pageNumber)
    {
        var requestPage = new RequestPage(pageNumber, PieValues.DefaultPageSize);
        var totalNoOfPies = await _pieService.GetPiesCountAsync();
        var pies = await _pieService.GetPagedPiesAsync(requestPage);

        return new PaginatedPieViewModel(pies, totalNoOfPies, requestPage.PageNumber);
    }

    private async Task<PaginatedPieViewModel> CreatePaginatedSortedViewModel(
        int? pageNumber, PieSortOption sortOption)
    {
        var requestPage = new RequestPage(pageNumber, PieValues.DefaultPageSize);
        var totalNoOfPies = await _pieService.GetPiesCountAsync();
        var pies = await _pieService.GetSortedPaginatedPiesAsync(requestPage, sortOption);

        return new PaginatedPieViewModel(pies, totalNoOfPies, pageNumber)
        {
            IdSortParam = GetSortParam(PieSortOption.IdAsc, sortOption),
            CategoryIdSortParam = GetSortParam(PieSortOption.CategoryIdAsc, sortOption),
            NameSortParam = GetSortParam(PieSortOption.NameAsc, sortOption),
            CategoryNameSortParam = GetSortParam(PieSortOption.CategoryNameAsc, sortOption),
            PriceSortParam = GetSortParam(PieSortOption.PriceAsc, sortOption),
            CurrentSort = Enum.GetName(typeof(PieSortOption), sortOption)!
        };
    }

    private static string GetSortParam(PieSortOption baseOption, PieSortOption currentOption) =>
        currentOption == baseOption
            ? baseOption.GetCounterpart().ToString()
            : baseOption.ToString();

    private async Task<IEnumerable<SelectListItem>> GetCategorySelectListAsync(int? selectedId = null)
    {
        var catDtoList = await _catService
            .GetCategoriesAsync();

        return GetSelectList(catDtoList, selectedId);
    }

    private readonly ICategoryService _catService = catService
            ?? throw new ArgumentNullException(nameof(catService), GeneralValues.ArgumentNullError);
    private readonly IPieService _pieService = pieService
            ?? throw new ArgumentNullException(nameof(pieService), GeneralValues.ArgumentNullError);
}
