namespace BethanysPieShop.IntegrationTests.Configurations;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(Environments.Development);

        builder.ConfigureServices(services =>
        {
            // Remove any existing DbContextOptions registrations (SqlServer, etc.)
            var dbContextDescriptor = services.FirstOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<PieShopDbContext>));
            if (dbContextDescriptor != null)
            {
                services.Remove(dbContextDescriptor);
            }

            // Remove the DbContext itself if previously added
            var dbContextService = services.FirstOrDefault(
                d => d.ServiceType == typeof(PieShopDbContext));
            if (dbContextService != null)
            {
                services.Remove(dbContextService);
            }

            // Register InMemory provider exclusively
            services.AddDbContext<PieShopDbContext>(options =>
            {
                options.UseInMemoryDatabase("IntegrationTestsDB");
            });

            // Build the provider and seed data
            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<PieShopDbContext>();

            // Ensure clean state for each test run
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            SeedData(db);
        });
    }

    private static void SeedData(PieShopDbContext db)
    {
        if (!db.Categories.Any())
        {
            db.Categories.AddRange(
                new Category { Id = 1, Name = "Fruit" },
                new Category { Id = 2, Name = "Chocolate" },
                new Category { Id = 3, Name = "Seasonal" }
            );
        }

        if (!db.Pies.Any())
        {
            db.Pies.AddRange(
                new Pie { Id = 1, Name = "Apple Pie", CategoryId = 1 },
                new Pie { Id = 3, Name = "Cherry Pie", CategoryId = 1 },
                new Pie { Id = 7, Name = "Chocolate Dream", CategoryId = 2 },
                new Pie { Id = 12, Name = "Pumpkin Pie", CategoryId = 3 },
                new Pie { Id = 16, Name = "Pecan Pie", CategoryId = 3 }
            );
        }
        db.SaveChanges();
    }

    internal void SeedData() => throw new NotImplementedException();
}
