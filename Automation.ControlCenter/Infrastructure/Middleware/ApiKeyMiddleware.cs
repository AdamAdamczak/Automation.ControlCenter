using Automation.ControlCenter.DTOs;
using System.Net;
using System.Text.Json;

namespace Automation.ControlCenter.Infrastructure.Middleware;

public class ApiKeyMiddleware
{
    private const string HeaderName = "X-API-KEY";
    private readonly RequestDelegate _next;
    private readonly string _apiKey;

    public ApiKeyMiddleware(
        RequestDelegate next,
        IConfiguration configuration)
    {
        _next = next;
        _apiKey = configuration["Security:ApiKey"]
            ?? throw new InvalidOperationException("API Key not configured");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(HeaderName, out var providedKey))
        {
            await RejectAsync(context, "API_KEY_MISSING");
            return;
        }

        if (!string.Equals(providedKey, _apiKey))
        {
            await RejectAsync(context, "API_KEY_INVALID");
            return;
        }

        await _next(context);
    }

    private static async Task RejectAsync(
        HttpContext context,
        string errorCode)
    {
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse
        {
            ErrorCode = errorCode,
            Message = "Unauthorized request"
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}
