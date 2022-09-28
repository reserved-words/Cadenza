using Cadenza.Apps.WindowsService.Interfaces;
using Cadenza.Apps.WindowsService.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Cadenza.Apps.WindowsService;

public class Worker : BackgroundService
{
    private readonly IEnumerable<IService> _services;
    private readonly IOptions<ServiceSettings> _settings;

    public Worker(IEnumerable<IService> services, IOptions<ServiceSettings> settings)
    {
        _services = services;
        _settings = settings;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var runFrequency = GetRunFrequency();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                foreach (var updater in _services)
                {
                    await updater.Run();

                    if (stoppingToken.IsCancellationRequested)
                        return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

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