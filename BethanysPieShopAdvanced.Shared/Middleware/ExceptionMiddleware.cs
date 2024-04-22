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

    private Task HandlesExceptionAsync(HttpContext context, Exception exception)
    {
        // Below, add more exception types as needed
        context.Response.ContentType = GeneralValues.JsonMediaType;
        context.Response.StatusCode = MapStatusCode(exception);  
        var result = JsonSerializer.Serialize(new { message = exception.Message });

        return context.Response.WriteAsync(result);
    }

    private int MapStatusCode(Exception exception)
    {
        var result = exception switch
        {
            ArgumentNullException => HttpStatusCode.BadRequest,
            ArgumentException => HttpStatusCode.BadRequest,
            CategoryNotEmptyException => HttpStatusCode.BadRequest,
            EntityDuplicationException<Pie> => HttpStatusCode.BadRequest,
            EntityDuplicationException<Category> => HttpStatusCode.BadRequest,
            EntityPropertyFormatException<Pie> => HttpStatusCode.BadRequest,
            EntityNotFoundException<Pie> => HttpStatusCode.NotFound,
            EntityNotFoundException<Category> => HttpStatusCode.NotFound,
            EntityIdMismatchException<Pie> => HttpStatusCode.Conflict,
            _ => HttpStatusCode.InternalServerError
        };

        return (int)result;
    }

    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionMiddleware> _logger = logger;
}

