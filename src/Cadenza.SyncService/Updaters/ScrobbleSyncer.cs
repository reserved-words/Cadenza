using Cadenza.Database.Interfaces;

namespace Cadenza.SyncService.Updaters;

internal class ScrobbleSyncer : IService
{
    private readonly IHistoryRepository _repository;

    public ScrobbleSyncer(IHistoryRepository repository)
    {
        _repository = repository;
    }

    public async Task Run()
    {
        await _repository.SyncScrobbles();
    }
}
