using Cadenza.Utilities;
using Microsoft.Extensions.Options;

namespace Cadenza.Common;

public class Logger : ILogger
{
    private readonly IHttpHelper _httpClient;
    private readonly IOptions<LoggerOptions> _options;

    public Logger(IOptions<LoggerOptions> options, IHttpHelper httpClient)
    {
        _options = options;
        _httpClient = httpClient;
    }

    public async Task LogError(Exception ex)
    {
        var data = GetErrorData(ex.Message, ex.StackTrace);
        await Log(data);
    }

    public async Task LogError(string message)
    {
        var data = GetErrorData(message, null);
        await Log(data);
    }

    public async Task LogInfo(string message)
    {
        var data = GetErrorData(message, null, true);
        await Log(data);
    }

    private async Task Log(object data)
    {
        var url = _options.Value.Url;
        await _httpClient.Post(url, null, data);
    }

    private object GetErrorData(string message, string stackTrace, bool info = false)
    {
        return new
        {
            app = GetAppName(),
            message = message,
            level = info ? 1 : 3,
            stackTrace = stackTrace
        };
    }

    private string GetAppName()
    {
        var env = _options.Value.Environment == "LIVE" 
            ? "" 
            : $" ({_options.Value.Environment})";

        return $"{_options.Value.ApplicationName}{env}";
    }
}