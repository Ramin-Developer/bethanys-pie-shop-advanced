namespace BethanysPieShop.IntegrationTest.Configuration;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var scopedService = scope.ServiceProvider;
            var dbcontext = scopedService.GetRequiredService<PieShopDbContext>();
        });
    }

    public void SeedData()
    {
        using var scope = Services.CreateScope();
        var dbContext = scope
            .ServiceProvider
            .GetRequiredService<PieShopDbContext>();

        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        var dbInitializer = new DbInitializer(dbContext);
        dbInitializer.Seed();
    }
}
