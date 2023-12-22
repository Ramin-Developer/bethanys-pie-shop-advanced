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
        _ = builder.Services.AddAutoMapper(typeof(AdminProfileMapping));
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

        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<PieShopDbContext>();

        // Seed the database
        var dbInitializer = new DbInitializer(context);
        dbInitializer.Seed();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        // Healthy Check
        app.UseHealthChecks("/health");

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }
}
