namespace BethanysPieShop.WebApi.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
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
        // Code 500 if unexpected
        var code = HttpStatusCode.InternalServerError; 

        // Below, add more exception types as needed
        if (exception is InvalidEntityIdException)
            code = HttpStatusCode.BadRequest;
        else if (exception is EntityNotFoundException)
            code = HttpStatusCode.NotFound;

        var result = JsonSerializer
            .Serialize(new { error = exception.Message });

        context.Response.ContentType = GeneralValues.JsonMediaType;
        context.Response.StatusCode = (int)code;
        
        return context.Response.WriteAsync(result);
    }

    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;
}
