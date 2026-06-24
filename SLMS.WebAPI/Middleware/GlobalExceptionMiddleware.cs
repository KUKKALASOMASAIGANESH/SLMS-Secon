using System.Text.Json;

namespace SLMS.WebAPI.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
             context.Response.ContentType =
                "application/json";

            var response = new
            {
                Success = false,
                Message = ex.Message,
                InnerException =
          ex.InnerException?.ToString()
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}