namespace Cadenza.Local.API;

public class LoggerConfig : ILoggerConfig
{
    private readonly IConfiguration _config;

    public LoggerConfig(IConfiguration config)
    {
        _config = config;
    }

    public string ApplicationName => _config.GetSection("Logger").GetSection("LocalAPI").GetValue<string>("ApplicationName");

    public string Url => _config.GetSection("Logger").GetValue<string>("Url");
}