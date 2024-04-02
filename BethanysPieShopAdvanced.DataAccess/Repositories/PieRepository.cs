namespace BethanysPieShop.DataAccess.Repositories;

public class PieRepository(PieShopDbContext dbContext) : IPieRepository
{
    public IQueryable<Pie> GetPies() =>
        _dbContext
            .Pies
            .Include(p => p.Category)
            .OrderBy(p => p.Name)
            .AsNoTracking()
            ?? Enumerable.Empty<Pie>().AsQueryable();

    public IQueryable<Pie> GetPagedPies(RequestPage requestPage)
    {
        // Default sort
        var pies = GetSortedPies(PieSortOption.IdAsc);
        var pagedPies = ApplyPaging(pies, requestPage);

        return pagedPies
            .AsNoTracking()
            ?? Enumerable.Empty<Pie>().AsQueryable();
    }

    public IQueryable<Pie> SearchPies(string searchQuery, int? catId)
    {
        var pies = _dbContext.Pies.AsQueryable();

        if (string.IsNullOrEmpty(searchQuery) == false)
        {
            pies = pies
                .Where(IncludeSearchConditions(searchQuery));
        }

        if (catId != null)
            pies = pies.Where(s => s.CategoryId == catId);

        return pies
            ?? Enumerable.Empty<Pie>().AsQueryable();
    }

    public async Task<Pie?> GetPieByIdAsync(int id) =>
        await _dbContext
            .Pies
            .Include(p => p.Category)
            .Include(p => p.Ingredients)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Pie?> GetPieByNameAsync(string name)
    {
        return await _dbContext
            .Pies
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<int> GetNoOfPiesAsync() =>
        await _dbContext
            .Pies
            .CountAsync();

    public async Task<int> AddPieAsync(Pie pie)
    {
        await
            _dbContext
            .Pies
            .AddAsync(pie);

        var noOfRows = await
            _dbContext
            .SaveChangesAsync();

        return noOfRows;
    }

    public async Task<int> UpdatePieAsync(Pie updatedPie)
    {
        // Check if the Pie exists before attempting to update it
        var existingPie = await _dbContext.Pies.FindAsync(updatedPie.Id)
            ?? throw new Exception("Pie not found.");

        // If the Pie exists, proceed to update it
        _dbContext.Entry(existingPie).CurrentValues.SetValues(updatedPie);
        try
        {
            var rowsAffected = await _dbContext.SaveChangesAsync();

            return rowsAffected;
        }
        catch (DbUpdateConcurrencyException)
        {
            // Handle concurrency exception as shown in earlier examples

            return 0;
        }
    }

    public async Task<int> DeletePieAsync(int id)
    {
        var pieToDelete = await _dbContext
            .Pies
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pieToDelete == null)
            return 0;

        _dbContext.Pies.Remove(pieToDelete);

        return await _dbContext.SaveChangesAsync();
    }

    public IQueryable<Pie> GetSortedPies(PieSortOption sortOption)
    {
        var pies = GetPies();
        var sortedPies = sortOption switch
        {
            PieSortOption.IdAsc => pies.OrderBy(p => p.Id),
            PieSortOption.IdDesc => pies.OrderByDescending(p => p.Id),
            PieSortOption.CategoryIdAsc => pies.OrderBy(p => p.CategoryId),
            PieSortOption.CategoryIdDesc => pies.OrderByDescending(p => p.CategoryId),
            PieSortOption.NameAsc => pies.OrderBy(p => p.Name),
            PieSortOption.NameDesc => pies.OrderByDescending(p => p.Name),
            PieSortOption.PriceAsc => pies.OrderBy(p => p.Price),
            PieSortOption.PriceDesc => pies.OrderByDescending(p => p.Price),
            PieSortOption.CategoryNameAsc => pies.OrderBy(
                p => (p.Category != null) ? p.Category.Name : string.Empty),

            PieSortOption.CategoryNameDesc => pies.OrderByDescending(
                p => (p.Category != null) ? p.Category.Name : string.Empty),

            // Default
            _ => pies.OrderBy(p => p.Id),
        };

        return sortedPies;
    }

    public IQueryable<Pie> GetSortedPaginatedPies(RequestPage requestPage, PieSortOption sortOption)
    {
        var pies = GetSortedPies(sortOption);

        return ApplyPaging(pies, requestPage);
    }

    public IQueryable<Pie> ApplyPaging(IQueryable<Pie> pies, RequestPage requestPage)
    {
        if (requestPage == null)
            throw new ArgumentNullException(nameof(requestPage));

        var pageNumber = requestPage.PageNumber;
        var pageSize = requestPage.PageSize;

        return pies
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    private Expression<Func<Pie, bool>> IncludeSearchConditions(string searchQuery)
    {
        return pie =>
            pie.Name.Contains(searchQuery) ||
            pie.ShortDescription != null && pie.ShortDescription.Contains(searchQuery) ||
            pie.LongDescription != null && pie.LongDescription!.Contains(searchQuery);
    }

    private readonly PieShopDbContext _dbContext = dbContext
        ?? throw new ArgumentNullException(nameof(dbContext), GeneralValues.ArgumentNullError);
}
