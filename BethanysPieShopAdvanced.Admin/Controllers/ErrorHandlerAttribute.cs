namespace BethanysPieShop.Admin.Controllers;

[AttributeUsage(AttributeTargets.Class)]
public class ErrorHandlerAttribute : Attribute, IFilterFactory
{
    public ErrorHandlerAttribute(ILogger<ErrorHandlerAttribute> logger) =>
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<ErrorHandlerAttribute>>();
        return new ErrorHandlerFilter(logger);
    }

    public bool IsReusable => false;

    private readonly ILogger<ErrorHandlerAttribute> _logger;
}
