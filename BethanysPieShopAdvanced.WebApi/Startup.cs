namespace BethanysPieShop.WebApi;

public static class Startup
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        // Call AddSharedSevices from SharedConfiguration project
        builder.AddServices();

        // Add services to the container.
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMemoryCache();

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
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
        // Adjust the middleware based on the environment.
        // For testing, you might want to ensure the ExceptionMiddleware is always used.
        if (app.Environment.IsEnvironment("Test"))
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
        else if (app.Environment.IsDevelopment())
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            // Use a generic exception handler in production.
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }


        app.UseRouting();
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        // Add health checks
        app.UseHealthChecks("/health");
    }
}
