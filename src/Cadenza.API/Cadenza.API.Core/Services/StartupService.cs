using Cadenza.API.Interfaces;

namespace Cadenza.API.Core.Services;

internal class StartupService : IStartupService
{
    private readonly ICachePopulater _populater;

    public StartupService(ICachePopulater populater)
    {
        _populater = populater;
    }

    public async Task Populate()
    {
        await _populater.Populate(false);
    }
}
