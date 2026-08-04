namespace BethanysPieShop.IntegrationTests.Configurations;

public sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName = $"IntegrationTestsDb_{Guid.NewGuid()}";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        builder.ConfigureServices(services =>
        {
            var dbOptionsDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<PieShopDbContext>));
            if (dbOptionsDescriptor is not null)
                services.Remove(dbOptionsDescriptor);

            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(PieShopDbContext));
            if (dbContextDescriptor is not null)
                services.Remove(dbContextDescriptor);

            services.AddDbContext<PieShopDbContext>(options =>
                options.UseInMemoryDatabase(_dbName));

            using var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<PieShopDbContext>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var initializer = new InMemoryDbInitializer(db);
            initializer.SeedInMemoryDb();
        });
    }

    public void ResetDb()
    {
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<PieShopDbContext>();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
        var initializer = new InMemoryDbInitializer(db);
        initializer.SeedInMemoryDb();
    }
}
