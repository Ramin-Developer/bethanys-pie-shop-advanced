namespace BethanysPieShop.WebApi.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Below, add more exception types as needed
        var statusCode = exception switch
        {
            ArgumentNullException => HttpStatusCode.BadRequest,
            InvalidEntityIdException => HttpStatusCode.BadRequest,
            EntityNotFoundException => HttpStatusCode.NotFound,
            _ => HttpStatusCode.InternalServerError,
        };

        var result = JsonSerializer
            .Serialize(new { error = exception.Message });

        context.Response.ContentType = GeneralValues.JsonMediaType;
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(result);
    }

    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;
}
