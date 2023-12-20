namespace BethanysPieShop.SharedConfiguration.Configuration;

public static class ServiceConfiguration
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        _ = builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.DictionaryKeyPolicy = null;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        });

        _ = builder.Services.AddDbContext<PieShopDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString(GeneralValues.ConnectionStringKey)));

        _ = builder.Services.AddScoped<IPieService, PieService>();
        _ = builder.Services.AddScoped<ICategoryService, CategoryService>();
        _ = builder.Services.AddScoped<IOrderService, OrderService>();

        _ = builder.Services.AddScoped<IPieRepository, PieRepository>();
        _ = builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        _ = builder.Services.AddScoped<IOrderRepository, OrderRepository>();

        _ = builder.Services.AddAutoMapper(typeof(PieProfileMapping));

        builder.Services.AddHealthChecks();
    }
}
