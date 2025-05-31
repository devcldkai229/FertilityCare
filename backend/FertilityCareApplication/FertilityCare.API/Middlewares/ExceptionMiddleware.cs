
using FertilityCare.Shared.Exceptions;

namespace FertilityCare.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BaseException e)
        {
            _logger.LogError(e, e.Message);
            context.Response.StatusCode = e.StatusCode;
            await context.Response.WriteAsJsonAsync(new { error = e.Message });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An unexpected error occurred.");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { error = "An unexpected error occurred." });
        }
    }

}
