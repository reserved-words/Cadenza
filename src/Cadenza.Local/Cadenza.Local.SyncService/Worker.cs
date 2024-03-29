﻿namespace Cadenza.Local.SyncService;

internal class Worker : BackgroundService
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

            foreach (var updater in _services)
            {
                await TryRun(updater);

                if (stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Processing cancelled");
                    return;
                }
            }

            _logger.LogInformation("Finished processing");
            _logger.LogInformation("---------------------------------------------");

            await Task.Delay(runFrequency, stoppingToken);
        }
    }

    private async Task TryRun(IService updater)
    {
        try
        {
            await updater.Run();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Processing errored");
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