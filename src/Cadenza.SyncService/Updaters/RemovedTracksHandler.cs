using Cadenza.Domain.Enums;
using Cadenza.SyncService.Interfaces;

namespace Cadenza.SyncService.Updaters;

internal class RemovedTracksHandler : IUpdateService
{
    private readonly IDatabaseRepository _database;
    private readonly IEnumerable<ISourceRepository> _sources;

    public RemovedTracksHandler(IDatabaseRepository database, IEnumerable<ISourceRepository> spurces)
    {
        _database = database;
        _sources = spurces;
    }

    public async Task Run()
    {
        foreach (var repository in _sources)
        {
            await RemoveTracks(repository, repository.Source);
        }
    }

    private async Task RemoveTracks(ISourceRepository repository, LibrarySource source)
    {
        var dbTracks = await _database.GetAllTracks(source);
        var sourceTracks = await repository.GetAllTracks();
        var removedTracks = dbTracks.Except(sourceTracks).ToList();

        if (removedTracks.Any())
        {
            await _database.RemoveTracks(source, removedTracks);
        }
    }
}