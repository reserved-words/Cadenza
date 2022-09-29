using Cadenza.Common.Domain.Enums;

namespace Cadenza.SyncService.Updaters;

internal class AddedTracksHandler : IService
{
    private readonly IDatabaseRepository _database;
    private readonly IEnumerable<ISourceRepository> _sources;

    public AddedTracksHandler(IDatabaseRepository database, IEnumerable<ISourceRepository> spurces)
    {
        _database = database;
        _sources = spurces;
    }

    public async Task Run()
    {
        foreach (var repository in _sources)
        {
            await AddTracks(repository, repository.Source);
        }
    }

    private async Task AddTracks(ISourceRepository repository, LibrarySource source)
    {
        var dbTracks = await _database.GetAllTracks(source);
        var sourceTracks = await repository.GetAllTracks();

        var addedTracks = sourceTracks.Except(dbTracks);

        foreach (var trackId in addedTracks)
        {
            var track = await repository.GetTrack(trackId);
            await _database.AddTrack(source, track);
        }
    }
}
