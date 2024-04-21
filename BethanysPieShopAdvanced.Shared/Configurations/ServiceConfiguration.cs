namespace BethanysPieShop.Shared.Configurations;

public static class ServiceConfiguration
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        AddControllerSettings(builder);
        AddDbContext(builder);
        AddRepositories(builder);
        AddBusinessServices(builder);
        AddAutoMapper(builder);

        builder.Services.AddHealthChecks();
    }

    private static void AddControllerSettings(WebApplicationBuilder builder)
    {
        _ = builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.DictionaryKeyPolicy = null;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        });
    }

    private static void AddDbContext(WebApplicationBuilder builder) => builder
            .Services
            .AddDbContext<PieShopDbContext>(options =>
                options
                .UseSqlServer(builder.Configuration.GetConnectionString(GeneralValues.ConnectionStringKey)));

    private static void AddRepositories(WebApplicationBuilder builder)
    {
        _ = builder.Services.AddScoped<IPieRepository, PieRepository>();
        _ = builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        _ = builder.Services.AddScoped<IOrderRepository, OrderRepository>();
    }

    private static void AddBusinessServices(WebApplicationBuilder builder)
    {
        _ = builder.Services.AddScoped<IPieService, PieService>();
        _ = builder.Services.AddScoped<ICategoryService, CategoryService>();
        _ = builder.Services.AddScoped<IOrderService, OrderService>();
    }

    private static void AddAutoMapper(WebApplicationBuilder builder)
    {
        _ = builder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<CategoryProfileMapping>();
            cfg.AddProfile<PieProfileMapping>();
            cfg.AddProfile<OrderLineProfileMapping>();
            cfg.AddProfile<OrderProfileMapping>();
        });
    }
}
