namespace BethanysPieShop.IntegrationTest.Configurations;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            // Explicitly set the environment name for the application during testing
            // Or "Test" if you have specific configurations for tests
            context.HostingEnvironment.EnvironmentName = "Development"; 
        });

        builder.ConfigureServices(services =>
        {
            // Remove any existing DbContextOptions configured for PieShopDbContext
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<PieShopDbContext>));

            if (descriptor != null)
                services.Remove(descriptor);

            // Configure your DbContext to use an in-memory database
            services.AddDbContext<PieShopDbContext>(options =>
            {
                options.UseInMemoryDatabase($"InMemoryDbForTesting_{DynamicDbName}");
            });
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

        var dbInitializer = new InMemoryDbInitializer(dbContext);
        dbInitializer.SeedInMemoryDb();
    }

    private readonly string DynamicDbName = Guid.NewGuid().ToString();
}
