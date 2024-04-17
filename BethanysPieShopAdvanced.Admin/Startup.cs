namespace BethanysPieShop.Admin;

public static class Startup
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.AddServices();

        // Add the filter to the MVC service configuration.
        _ = builder.Services.AddControllersWithViews(options =>
        {
            options.Filters.Add<ErrorHandlerFilter>();
        });

        _ = builder.Services.AddScoped<IPieHelperService, PieHelperService>();
        _ = builder.Services.AddScoped<IPieModelErrorService, PieModelErrorService>();
        _ = builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // Adding mappings
        _ = builder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<CategoryProfileMapping>();
            cfg.AddProfile<PieMapping>();
        });
    }

    public static void Configure(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        // Routing should come before authorization and endpoint-dependent middleware
        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        // Healthy Check
        app.UseHealthChecks("/health");

        // Initialize the SQL Server database
        InitializeDatabase(app);
    }

    private static void InitializeDatabase(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var dbContext = services.GetRequiredService<PieShopDbContext>();
            var sqlServerInitializer = new SqlServerDbInitializer(dbContext);
            sqlServerInitializer.Initialize();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred initializing the SQL Server database.");
        }
    }
}
