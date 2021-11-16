﻿namespace Cadenza.Local.SyncService;

public class Worker : BackgroundService
{
    private readonly IConfiguration _config;
    private readonly IUpdater _updater;

    public Worker(IUpdater updater, IConfiguration config)
    {
        _config = config;
        _updater = updater;
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
                _updater.UpdateDeletedFiles();

                if (stoppingToken.IsCancellationRequested)
                    return;

                _updater.UpdateAddedFiles();

                if (stoppingToken.IsCancellationRequested)
                    return;

                _updater.UpdateModifiedFiles();

                if (stoppingToken.IsCancellationRequested)
                    return;

                _updater.RemovePlayedFiles();

                if (stoppingToken.IsCancellationRequested)
                    return;

                _updater.ProcessUpdateQueue();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            await Task.Delay(runFrequencyInMilliseconds, stoppingToken);
        }

    }
}