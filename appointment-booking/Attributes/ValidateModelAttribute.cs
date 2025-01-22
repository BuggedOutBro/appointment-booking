using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace appointment_booking.Attributes;

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(kvp => kvp.Value?.Errors?.Any() == true)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            var errorResponse = new
            {
                title = "One or more validation errors occurred.",
                status = StatusCodes.Status400BadRequest,
                errors,
                traceId = context.HttpContext.TraceIdentifier
            };
            context.Result = new ContentResult
            {
                Content = System.Text.Json.JsonSerializer.Serialize(errorResponse),
                ContentType = "application/json",
                StatusCode = StatusCodes.Status400BadRequest
            };

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
}
