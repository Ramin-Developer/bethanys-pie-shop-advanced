namespace BethanysPieShop.Common.Pagination;

public class RequestPage
{
    public RequestPage(int? pageNumber, int? pageSize = null)
    {
        PageNumber = pageNumber ?? PieValues.DefaultPageNumber;
        PageSize = pageSize ?? PieValues.DefaultPageSize;
    }

    public int PageNumber { get; }

    public int PageSize { get; }
}
