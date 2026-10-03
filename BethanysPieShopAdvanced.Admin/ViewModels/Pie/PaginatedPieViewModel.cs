namespace BethanysPieShop.Admin.ViewModels.Pie;

public class PaginatedPieViewModel(List<PieDto> itemList, int totalItemCount, int? currentPage, int? pageSize = null)
{
    public List<PieDto> Pies => [.. _data];

    public int TotalItemsCount { get; } = totalItemCount;

    public int FirstPage => PieValues.DefaultPageNumber;

    public int LastPage => NoOfPages;

    public int? CurrentPage { get; } = currentPage ?? PieValues.DefaultPageNumber;

    public int PreviousPage => CurrentPage!.Value - 1;

    public int NextPage => CurrentPage!.Value + 1;

    public int PageSize { get; } = pageSize ?? PieValues.DefaultPageSize;

    public int NoOfPages => (int)Math.Ceiling((double)TotalItemsCount / PageSize);

    public bool HasPreviousPage => CurrentPage > 1;

    public bool HasNextPage => CurrentPage < NoOfPages;

    public bool IsNullOrEmpty => _data == null || _data.Any() == false;

    public int Count => _data.Count;

    public string FirstBtnEnabled => HasPreviousPage ? "enabled" : "disabled";

    public string LastBtnEnabled => HasNextPage ? "enabled" : "disabled";

    public string PreviousBtnEnabled => HasPreviousPage ? "enabled" : "disabled";

    public string NextBtnEnabled => HasNextPage ? "enabled" : "disabled";

    public string IdSortParam { get; set; } = string.Empty;

    public string CategoryIdSortParam { get; set; } = string.Empty;

    public string NameSortParam { get; set; } = string.Empty;

    public string CategoryNameSortParam { get; set; } = string.Empty;

    public string PriceSortParam { get; set; } = string.Empty;

    public string CurrentSort { get; set; } = string.Empty;

    public IEnumerator<PieDto> GetEnumerator() => _data.GetEnumerator();

    private readonly List<PieDto> _data = itemList ?? [];
}
