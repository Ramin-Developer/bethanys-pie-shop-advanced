namespace BethanysPieShop.Admin.Controllers;

[AttributeUsage(AttributeTargets.Class)]
public class ErrorHandlerAttribute(ILogger<ErrorHandlerAttribute> logger) : Attribute, IFilterFactory
{
    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<ErrorHandlerAttribute>>();
        return new ErrorHandlerFilter(logger);
    }

    public bool IsReusable => false;

    private readonly ILogger<ErrorHandlerAttribute> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
}
