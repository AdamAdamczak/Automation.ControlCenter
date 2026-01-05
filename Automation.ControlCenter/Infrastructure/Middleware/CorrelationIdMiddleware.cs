using System.Diagnostics;

namespace Automation.ControlCenter.Infrastructure.Middleware;

public class CorrelationIdMiddleware
{
    private const string HeaderName = "X-Correlation-Id";
    private readonly RequestDelegate _next;
    private readonly ILogger<CorrelationIdMiddleware> _logger;

    public CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers.TryGetValue(
            HeaderName,
            out var value)
            ? value.ToString()
            : Guid.NewGuid().ToString();

        context.Items[HeaderName] = correlationId;
        context.Response.Headers[HeaderName] = correlationId;

        using (Activity.Current = new Activity("Request"))
        {
            Activity.Current.SetIdFormat(ActivityIdFormat.W3C);
            Activity.Current.Start();
            Activity.Current.AddTag(HeaderName, correlationId);

            await _next(context);
        }
    }
}
