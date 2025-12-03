namespace BethanysPieShop.Admin.Helpers;

public interface IPieHelperService
{
    IEnumerable<SelectListItem> GetSelectList(IEnumerable<CategoryDto> categories, int? selectedId);

    Task PopulateCategorySelectListAsync(PieAddViewModel pieAddViewModel);

    Task<PieSearchViewModel> CreatePieSearchViewModelAsync(string? searchQuery, int? searchCategory);

    Task<PieEditViewModel> CreatePieUpdateViewModelAsync(
        PieDto? pieToUpdate, int? selectedId = null!, string errorMessage = null!);

    Task<PieAddViewModel> CreatePieAddViewModelAsync();

    Task<PaginatedPieViewModel> CreatePaginatedSortedViewModel(
        int? pageNumber, PieGroupOption groupOption, PieSortOption sortOption);
}
