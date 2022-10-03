namespace Cadenza.API.Core;

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
