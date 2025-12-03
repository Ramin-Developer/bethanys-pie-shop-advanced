namespace BethanysPieShop.IntegrationTests.Utilities;

public class ScopedDbContext : IDisposable
{
    public static ScopedDbContext Create(IServiceScopeFactory scopeFactory)
    {
        var scope = scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<PieShopDbContext>();
        
        return new ScopedDbContext(scope, dbContext);
    }

    public void Dispose() => _scope.Dispose();

    public PieShopDbContext DbContext { get; }

    private ScopedDbContext(IServiceScope scope, PieShopDbContext dbContext)
    {
        _scope = scope;
        DbContext = dbContext;
    }

    private readonly IServiceScope _scope;
}
