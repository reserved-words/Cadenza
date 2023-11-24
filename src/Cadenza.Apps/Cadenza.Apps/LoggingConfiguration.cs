using Microsoft.Extensions.Configuration;

namespace Cadenza.Apps;

public static class LoggingConfiguration
{
    public static string LogFilePath(this IConfiguration config)
    {
        var loggingConfig = config.GetSection("Serilog");
        var logPathFormat = loggingConfig.GetValue<string>("LogPathFormat") ?? "";
        var applicationName = loggingConfig.GetValue<string>("ApplicationName");
        var logFilePath = string.Format(logPathFormat, applicationName);
        return logFilePath;
    }
}
