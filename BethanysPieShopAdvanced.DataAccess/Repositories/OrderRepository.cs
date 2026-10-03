namespace BethanysPieShop.DataAccess.Repositories;

public class OrderRepository(PieShopDbContext dbContext) : IOrderRepository
{
    public async Task<IEnumerable<Order>> GetOrdersWithOrderLinesAsync() =>
        await
            _dbContext
            .Orders
            .Include(o => o.OrderLines)
            .ThenInclude(ol => ol.Pie)
            .OrderBy(o => o.Id)
            .ToListAsync()
            ?? [];

    public async Task<Order?> GetOrderDetailsAsync(int? orderId)
    {
        if (orderId == null)
            return null;

        return await
            _dbContext
            .Orders
            .Where(o => o.Id == orderId.Value)
            .Include(o => o.OrderLines)
            .ThenInclude(ol => ol.Pie)
            .OrderBy(o => o.Id)
            .FirstOrDefaultAsync();
    }

    private readonly PieShopDbContext _dbContext = dbContext
            ?? throw new ArgumentNullException(nameof(dbContext), GeneralValues.ArgumentNullError);
}
