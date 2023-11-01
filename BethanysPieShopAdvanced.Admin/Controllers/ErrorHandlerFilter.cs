namespace BethanysPieShop.Admin.Controllers;

public class ErrorHandlerFilter : IExceptionFilter
{
    public ErrorHandlerFilter(ILogger<ErrorHandlerAttribute> logger) =>
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;

        // Retrieve the action name for further context
        var actionValue = context.RouteData.Values["action"];
        var actionName = actionValue?.ToString() ?? string.Empty;

        // Log the exception
        _logger.LogError(exception, GeneralValues.GenericLogFormatError, actionName);

        var errorViewModel = new ErrorViewModel
        {
            ErrorMessage = $"An error occurred in the {actionName} action.",
            Details = exception.Message
        };

        context.Result = new ObjectResult(errorViewModel)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
    }

    private readonly ILogger<ErrorHandlerAttribute> _logger;
}