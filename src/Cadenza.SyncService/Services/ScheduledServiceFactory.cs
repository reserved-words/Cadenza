using Cadenza.SyncService.Model;
using Microsoft.Extensions.Options;

namespace Cadenza.SyncService.Services;

internal class ScheduledServiceFactory : IScheduledServiceFactory
{
    private readonly IService _updatesHandler;
    private readonly IService _syncHandler;
    private readonly IService _scrobbleSyncer;
    private readonly IService _scrobbler;

    private readonly IOptions<FrequencySecondsSettings> _settings;

    // TODO: Any way to make this generic? Inject a dictionary of keys and services, make sure keys match
    // frequency setting names. Then put this and Worker back in shared project for other services to use

    public ScheduledServiceFactory(
        IOptions<FrequencySecondsSettings> frequencySettings,
        [FromKeyedServices("LibrarySync")] IService syncHandler,
        [FromKeyedServices("LibraryUpdates")] IService updateRequestsHandler,
        [FromKeyedServices("ScrobbleSync")] IService scrobbleSyncer,
        [FromKeyedServices("Scrobbling")] IService scrobbler)
    {
        _updatesHandler = updateRequestsHandler;
        _syncHandler = syncHandler;
        _settings = frequencySettings;
        _scrobbleSyncer = scrobbleSyncer;
        _scrobbler = scrobbler;
    }

    public List<ScheduledService> GetScheduledServices()
    {
        var services = new List<ScheduledService>();
        services.Add(new ScheduledService { Service = _scrobbler, FrequencySeconds = _settings.Value.Scrobbling });
        services.Add(new ScheduledService { Service = _syncHandler, FrequencySeconds = _settings.Value.LibrarySync });
        services.Add(new ScheduledService { Service = _updatesHandler, FrequencySeconds = _settings.Value.LibraryUpdates });
        services.Add(new ScheduledService { Service = _scrobbleSyncer , FrequencySeconds = _settings.Value.ScrobbleSync });
        return services;
    }
}
