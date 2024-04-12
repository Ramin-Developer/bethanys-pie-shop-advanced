namespace BethanysPieShop.Common.Interfaces;

public interface IPieRepository
{
    // CRUD Operations on Pies
    // Todo: Find out why this method is synchronoius
    IQueryable<Pie> GetPies();

    Task<Pie?> GetPieByIdAsync(int id);

    Task<int> AddPieAsync(Pie pie);

    Task<int> UpdatePieAsync(Pie updatedPie);

    Task<int> DeletePieAsync(int id);

    // Other Operations, GetPieByName, Count, Paging, Sorting, Searching
    Task<Pie?> GetPieByNameAsync(string name);

    Task<int> GetPiesCountAsync();

    IQueryable<Pie> GetPagedPies(RequestPage requestPage);

    IQueryable<Pie> GetSortedPies(PieSortOption sortOption);

    IQueryable<Pie> GetSortedPaginatedPies(RequestPage requestPage, PieSortOption sortOption);

    IQueryable<Pie> SearchPies(string searchQuery, int? categoryId);
}
