namespace Cadenza.Local.SyncService;

internal class Worker : BackgroundService
{
    private readonly IConfiguration _config;
    private readonly IEnumerable<IUpdateService> _updaters;

    public Worker(IEnumerable<IUpdateService> updaters, IConfiguration config)
    {
        _config = config;
        _updaters = updaters;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var runFrequencyInMinutes = _config.GetValue<int>("RunFrequencyMinutes");
        var runFrequencyInSeconds = runFrequencyInMinutes * 60;
        var runFrequencyInMilliseconds = runFrequencyInSeconds * 1000;

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                foreach (var updater in _updaters)
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

            await Task.Delay(runFrequencyInMilliseconds, stoppingToken);
        }
    }
}