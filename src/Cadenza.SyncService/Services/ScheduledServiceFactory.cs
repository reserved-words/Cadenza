using Cadenza.SyncService.Model;
using Cadenza.SyncService.Updaters.Interfaces;
using Microsoft.Extensions.Options;

namespace Cadenza.SyncService.Services;

internal class ScheduledServiceFactory : IScheduledServiceFactory
{
    private readonly IUpdatesHandler _updatesHandler;
    private readonly ISyncHandler _syncHandler;
    private readonly IScrobbleSyncer _scrobbleSyncer;
    private readonly IScrobbler _scrobbler;

    private readonly IOptions<ServiceSettings> _settings;

    public ScheduledServiceFactory(IUpdatesHandler updateRequestsHandler, ISyncHandler syncHandler, IOptions<ServiceSettings> settings, IScrobbleSyncer scrobbleSyncer, IScrobbler scrobbler)
    {
        _updatesHandler = updateRequestsHandler;
        _syncHandler = syncHandler;
        _settings = settings;
        _scrobbleSyncer = scrobbleSyncer;
        _scrobbler = scrobbler;
    }

    public List<ScheduledService> GetScheduledServices()
    {
        var services = new List<ScheduledService>();
        services.Add(new ScheduledService { Service = _scrobbler, FrequencySeconds = _settings.Value.ScrobbleFrequencySeconds });
        services.Add(new ScheduledService { Service = _syncHandler, FrequencySeconds = _settings.Value.SyncFrequencySeconds });
        services.Add(new ScheduledService { Service = _updatesHandler, FrequencySeconds = _settings.Value.UpdateFrequencySeconds });
        services.Add(new ScheduledService { Service = _scrobbleSyncer , FrequencySeconds = _settings.Value.ScrobbleSyncFrequencySeconds });
        return services;
    }
}
