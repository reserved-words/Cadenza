using Cadenza.WindowsService.Interfaces;
using Cadenza.WindowsService.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Cadenza.WindowsService;

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
        var runFrequencyInMinutes = _settings.Value.RunFrequencyMinutes;
        var runFrequencyInSeconds = runFrequencyInMinutes * 60;
        var runFrequencyInMilliseconds = runFrequencyInSeconds * 1000;
        return runFrequencyInMilliseconds;
    }
}