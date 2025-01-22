using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace appointment_booking.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Validation error: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex, StatusCodes.Status400BadRequest, "Bad Request.");
        }
        catch (TimeoutException ex)
        {
            _logger.LogError(ex, "Timeout error: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex, StatusCodes.Status504GatewayTimeout, "Timeout occurred.");
        }
        catch (Exception ex) // Other unhandled exceptions
        {
            _logger.LogError(ex, "An unhandled exception occurred.");
            await HandleExceptionAsync(context, ex, StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex, int statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        return context.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            status = statusCode,
            title = message,
            detail = ex.Message,
            instance = context.TraceIdentifier
        }));
    }
}
