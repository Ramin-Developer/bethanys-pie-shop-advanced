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
        // ExceptionMiddleware must be at the top to catch exceptions thrown by any subsequent middleware
        // or MVC actions.
        app.UseMiddleware<ExceptionMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();

        // Add health checks
        app.UseHealthChecks("/health");
    }
}
