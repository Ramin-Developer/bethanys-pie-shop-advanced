namespace BethanysPieShop.Admin.Controllers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ErrorHandlerAttribute : Attribute, IFilterFactory
{
    // CreateInstance method is responsible for creating an instance of the filter.
    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
        // Get the logger specifically for ErrorHandlerFilter.
        var logger = serviceProvider.GetRequiredService<ILogger<ErrorHandlerFilter>>();

        // Return a new instance of the filter, injecting any dependencies it requires.
        return new ErrorHandlerFilter(logger);
    }

    // This property is necessary because IFilterFactory requires it.
    // It indicates whether multiple instances of your filter can be reused.
    public bool IsReusable => false;
}
