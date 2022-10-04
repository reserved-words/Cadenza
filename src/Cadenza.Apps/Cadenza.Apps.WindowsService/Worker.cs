using Microsoft.Extensions.Logging;

namespace Cadenza.Apps.WindowsService;

public class Worker : BackgroundService
{
    private readonly IEnumerable<IService> _services;
    private readonly IOptions<ServiceSettings> _settings;
    private readonly ILogger<Worker> _logger;

    public Worker(IEnumerable<IService> services, IOptions<ServiceSettings> settings, ILogger<Worker> logger)
    {
        _services = services;
        _settings = settings;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var runFrequency = GetRunFrequency();

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Started processing");

            try
            {
                foreach (var updater in _services)
                {
                    await updater.Run();

                    if (stoppingToken.IsCancellationRequested)
                    {
                        _logger.LogInformation("Processing cancelled");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Processing errored");
            }

            _logger.LogInformation("Finished processing");
            _logger.LogInformation("---------------------------------------------");

            await Task.Delay(runFrequency, stoppingToken);
        }
    }

    private int GetRunFrequency()
    {
        var minutes = _settings.Value.RunFrequencyMinutes;
        var seconds = minutes * 60;
        var milliseconds = seconds * 1000;
        return milliseconds;
    }
}