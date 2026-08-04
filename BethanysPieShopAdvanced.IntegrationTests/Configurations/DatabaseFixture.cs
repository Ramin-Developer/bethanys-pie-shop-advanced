namespace BethanysPieShop.IntegrationTests.Configurations;

public class DatabaseFixture : IDisposable
{
    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<PieShopDbContext>()
            .UseInMemoryDatabase(InMemoryDbName)
            .Options;

        DbContext = new PieShopDbContext(options);
    }

    public void Dispose()
    {
        DbContext.Dispose();
        GC.SuppressFinalize(this);
    }

    public PieShopDbContext DbContext { get; }

    private static string InMemoryDbName =>
        GeneralValues.DatabaseName + "_" + Guid.NewGuid().ToString();
}
