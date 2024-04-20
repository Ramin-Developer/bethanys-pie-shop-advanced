namespace BethanysPieShop.Shared.Middleware;

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
            await HandlesExceptionAsync(context, ex);
        }
    }

    private static Task HandlesExceptionAsync(HttpContext context, Exception exception)
    {
        // Below, add more exception types as needed
        context.Response.ContentType = GeneralValues.JsonMediaType;
        var statusCodes = exception switch
        {
            ArgumentNullException => HttpStatusCode.BadRequest,
            ArgumentException => HttpStatusCode.BadRequest,
            CategoryNotEmptyException => HttpStatusCode.BadRequest,
            EntityDuplicationException<Pie> => HttpStatusCode.BadRequest,
            EntityDuplicationException<Category> => HttpStatusCode.BadRequest,
            EntityIdFormatException<Pie> => HttpStatusCode.BadRequest,
            EntityNameFormatException<Pie> => HttpStatusCode.BadRequest,
            EntityNameFormatException<Category> => HttpStatusCode.BadRequest,
            EntityPropertyFormatException<Pie> => HttpStatusCode.BadRequest,
            EntityNotFoundException<Pie> => HttpStatusCode.NotFound,
            EntityNotFoundException<Category> => HttpStatusCode.NotFound,
            EntityIdFormatException<Category> => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError,
        };

        context.Response.StatusCode = (int)statusCodes;
        var result = JsonSerializer
            .Serialize(new { error = exception.Message });

        return context.Response.WriteAsync(result);
    }

    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;
}

