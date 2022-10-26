using Cadenza.Common.Interfaces.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Cadenza.Apps.API;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            _logger.LogInformation(context.Request.Path);
            _logger.LogError(error, "Unhandled API error");

            var response = context.Response;
            response.ContentType = "application/json";

            var jsonConverter = context.RequestServices.GetRequiredService<IJsonConverter>();
            var result = jsonConverter.Serialize(new { message = error?.Message });
            await response.WriteAsync(result);
        }
    }
}
