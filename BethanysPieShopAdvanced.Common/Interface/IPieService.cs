namespace BethanysPieShop.Common.Interface;

public interface IPieService
{
    Task<List<PieDto>> GetPiesAsync();

    Task<List<PieDto>> GetPagedPiesAsync(RequestPage requestPage);

    Task<List<PieDto>> GetSortedPaginatedPiesAsync(RequestPage requestPage, PieSortOption sortOption);

    Task<List<PieDto>> SearchPiesAsync(int? categoryId, string searchQuery);

    Task<PieDto?> GetPieByIdAsync(int id);

    Task<PieDto?> GetPieByNameAsync(string name);

    Task<int> GetPiesCountAsync();
    
    Task<int> AddPieAsync(PieDto pie);

    Task<int> UpdatePieAsync(PieDto UpdatedPie);

    Task<int> DeletePieAsync(int id);
}
