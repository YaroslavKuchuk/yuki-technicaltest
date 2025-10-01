using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BloggingSystem.Api.Middleware;

public sealed class ErrorHandlingMiddleware(RequestDelegate next, ProblemDetailsFactory problemFactory)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var (status, title) = ex switch
            {
                ArgumentException => (StatusCodes.Status400BadRequest, "Invalid argument"),
                InvalidOperationException => (StatusCodes.Status409Conflict, "Operation conflict"),
                KeyNotFoundException => (StatusCodes.Status404NotFound, "Not found"),
                _ => (StatusCodes.Status500InternalServerError, "Unexpected error")
            };

            var details = problemFactory.CreateProblemDetails(
                context,
                statusCode: status,
                title: title,
                detail: ex.Message,
                instance: context.Request.Path
            );
            details.Extensions["traceId"] = context.TraceIdentifier;

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = status;
            await context.Response.WriteAsJsonAsync(details);
        }
    }
}