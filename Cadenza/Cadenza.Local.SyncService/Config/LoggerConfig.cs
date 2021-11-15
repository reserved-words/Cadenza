namespace Cadenza.Local.SyncService;

public class LoggerConfig : ILoggerConfig
{
    private readonly IConfiguration _config;

    public LoggerConfig(IConfiguration config)
    {
        _config = config;
    }

    private IConfigurationSection _loggingSection => _config.GetSection("Logger");

    public string ApplicationName => _loggingSection.GetSection("SyncService").GetValue<string>("ApplicationName");

    public string Url => _loggingSection.GetValue<string>("Url");
}