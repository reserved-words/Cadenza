namespace Cadenza.Common;

public class Logger : ILogger
{
    private readonly IHttpClient _httpClient;
    private readonly ILoggerConfig _config;

    public Logger(ILoggerConfig config, IHttpClient httpClient)
    {
        _config = config;
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
        var url = _config.Url;
        await _httpClient.Post(url, null, data);
    }

    private object GetErrorData(string message, string stackTrace, bool info = false)
    {
        return new
        {
            app = _config.ApplicationName,
            message = message,
            level = info ? 1 : 3,
            stackTrace = stackTrace
        };
    }
}