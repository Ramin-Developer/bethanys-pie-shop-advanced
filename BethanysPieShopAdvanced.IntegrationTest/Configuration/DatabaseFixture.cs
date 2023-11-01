namespace BethanysPieShop.IntegrationTest.Configuration;

public class DatabaseFixture : IDisposable
{
    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<PieShopDbContext>()
            .UseInMemoryDatabase(DatabaseName)
            .Options;

        DbContext = new PieShopDbContext(options);
    }

    public void Dispose() => DbContext.Dispose();

    public PieShopDbContext DbContext { get; }

    private string DatabaseName =>
        GeneralValues.DatabaseName + "_" + Guid.NewGuid().ToString();
}
