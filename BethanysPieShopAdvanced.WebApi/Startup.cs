namespace BethanysPieShop.WebApi;

public static class Startup
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        // Call AddSharedSevices from SharedConfiguration project
        builder.AddServices();

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMemoryCache();

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        // Configure your DbContext with retry policy for handling transient
        // database concurrency exceptions
        builder.Services.AddDbContext<PieShopDbContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(
                GeneralValues.ConnectionStringKey,
                sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null);
                });
        });

    }

    public static void Configure(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        // Add health checks
        app.UseHealthChecks("/health");
    }
}
