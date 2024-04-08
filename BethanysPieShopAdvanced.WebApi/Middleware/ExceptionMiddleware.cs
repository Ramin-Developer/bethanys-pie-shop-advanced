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
            await HandlesExceptionAsync(context, ex);
        }
    }

    private static Task HandlesExceptionAsync(HttpContext context, Exception exception)
    {
        // Below, add more exception types as needed
        var statusCode = exception switch
        {
            ArgumentNullException => HttpStatusCode.BadRequest,
            ArgumentException => HttpStatusCode.BadRequest,
            CategoryNotEmptyException => HttpStatusCode.BadRequest,
            EntityDuplicateException<Pie> => HttpStatusCode.BadRequest,
            EntityDuplicateException<Category> => HttpStatusCode.BadRequest,
            InvalidEntityIdException<Pie> => HttpStatusCode.BadRequest,
            InvalidEntityNameException<Pie> => HttpStatusCode.BadRequest,
            InvalidEntityNameException<Category> => HttpStatusCode.BadRequest,
            EntityNotFoundException<Pie> => HttpStatusCode.NotFound,
            EntityNotFoundException<Category> => HttpStatusCode.NotFound,
            InvalidEntityIdException<Category> => HttpStatusCode.BadRequest,
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

