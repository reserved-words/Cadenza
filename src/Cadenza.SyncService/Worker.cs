using Cadenza.SyncService.Model;

namespace Cadenza.Local.SyncService;

internal class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IEnumerable<ScheduledService> _services;

    public Worker(IScheduledServiceFactory serviceFactory, ILogger<Worker> logger)
    {
        _logger = logger;
        _services = serviceFactory.GetScheduledServices();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            foreach (var service in _services)
            {
                await TryRun(service);

                if (stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Processing cancelled");
                    return;
                }
            }
        }
    }

    private async Task TryRun(ScheduledService service)
    {
        if (service.LastRun.HasValue)
        {
            if (service.LastRun.Value.AddSeconds(service.FrequencySeconds) > DateTime.Now)
            {
                return;
            }
        }

        var serviceName = service.Service.GetType().Name;

        try
        {
            _logger.LogInformation($"Running service {serviceName}");

            service.LastRun = DateTime.Now;

            await service.Service.Run();

            _logger.LogInformation($"Finished processing service {serviceName}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error running service {serviceName}");
        }

        _logger.LogInformation("---------------------------------------------");
    }
}