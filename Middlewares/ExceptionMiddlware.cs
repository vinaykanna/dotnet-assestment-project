using System.Text.Json;
using NewsApi.Exceptions;

namespace NewsApi.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }

        catch (NotFoundException ex)
        {

            _logger.LogError(
                ex,
                "Resource {ResourceName} with id {ResourceId} not found",
                ex.ResourceName,
                ex.ResourceId
            );

            httpContext.Response.StatusCode = 404;

            httpContext.Response.ContentType = "application/json";


            var response = new
            {
                message = ex.Message
            };

            await httpContext.Response.WriteAsync(
                JsonSerializer.Serialize(response)
            );
        }

        catch (UserAlreadyExistsException ex)
        {
            _logger.LogError(
                ex,
                "User with email {Email} already exists",
                ex.Email
            );

            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = 409;

            var response = new
            {
                message = ex.Message
            };

            await httpContext.Response.WriteAsync(
                JsonSerializer.Serialize(response)
            );
        }

        catch (ForbiddenException ex)
        {
            _logger.LogError(
                ex,
                "Forbidden"
            );

            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;

            await httpContext.Response.WriteAsJsonAsync(new
            {
                message = ex.Message
            });
        }

        catch (Exception ex)
        {
            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = "application/json";

            _logger.LogError(
                ex,
                "Error occured"
            );

            var response = new
            {
                message = "Internal Server Error"
            };

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}

