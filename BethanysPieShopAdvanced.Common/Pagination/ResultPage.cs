namespace BethanysPieShop.Common.Pagination;

public class ResultPage<T>
{
    public List<T> Results { get; set; } = new List<T>();

    public int ItemsCount { get; set; }

    public int PageSize { get; set; }

    public int CurrentPage { get; set; }

    public int PagesCount => (int)Math.Ceiling((double)ItemsCount / PageSize);
}
