namespace BethanysPieShop.Common.Interfaces;

public interface IPieService
{
    Task<List<PieDto>> GetPiesAsync();

    Task<List<PieDto>> GetPagedPiesAsync(RequestPage requestPage);

    Task<List<PieDto>> GetSortedPaginatedPiesAsync(RequestPage requestPage, PieSortOption sortOption);

    Task<List<PieDto>> SearchPiesAsync(int? categoryId, string searchQuery);

    Task<PieDto?> GetPieByIdAsync(int id);

    Task<PieDto?> GetPieByNameAsync(string name);

    Task<int> GetPiesCountAsync();
    
    Task<PieDto> AddPieAsync(PieDto pie);

    Task<int> UpdatePieAsync(int id, PieDto UpdatedPie);

    Task<int> DeletePieAsync(int id);
}
