namespace BethanysPieShop.Admin.ViewModels.Pie;

public class PaginatedPieViewModel
{
    public PaginatedPieViewModel(List<PieDto> itemList, int totalItemCount, int? currentPage, int? pageSize = null)
    {
        _data = itemList ?? new List<PieDto>();
        TotalItemsCount = totalItemCount;

        PageSize = pageSize ?? PieValues.DefaultPageSize;
        CurrentPage = currentPage ?? PieValues.DefaultPageNumber;
        NoOfPages = (int)Math.Ceiling((double)TotalItemsCount / PageSize);
    }

    public List<PieDto> Pies => _data.ToList();

    public int TotalItemsCount { get; }

    public int FirstPage => PieValues.DefaultPageNumber;

    public int LastPage => NoOfPages;

    public int? CurrentPage { get; }

    public int PreviousPage => CurrentPage!.Value - 1;

    public int NextPage => CurrentPage!.Value + 1;

    public int PageSize { get; }

    public int NoOfPages { get; }

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

    private readonly List<PieDto> _data;
}
