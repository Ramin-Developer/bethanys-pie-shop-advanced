namespace BethanysPieShop.SharedConfiguration.Configurations;

public static class LoggingConfiguration
{
    public static void ConfigureLogging(this WebApplicationBuilder builder)
    {
        // Shared logging configurations here, e.g.:
        _ = builder.Logging.ClearProviders();
        _ = builder.Logging.AddConsole();
        _ = builder.Logging.SetMinimumLevel(LogLevel.Information);

        // Other shared configurations
    }
}
