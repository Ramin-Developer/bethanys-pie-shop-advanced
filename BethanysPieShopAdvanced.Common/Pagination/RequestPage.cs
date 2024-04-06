namespace BethanysPieShop.Common.Pagination;

public class RequestPage(int? pageNumber, int? pageSize = null)
{
    public int PageNumber { get; } =
        pageNumber
        ?? PieValues
            .DefaultPageNumber;

    public int PageSize { get; } =
        pageSize
        ?? PieValues
            .DefaultPageSize;
}
