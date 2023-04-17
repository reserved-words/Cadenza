namespace Cadenza.SyncService.Updaters;

internal class RemovedTracksHandler : IService
{
    private readonly IDatabaseRepository _database;
    private readonly ILogger<RemovedTracksHandler> _logger;
    private readonly IEnumerable<ISourceRepository> _sources;

    public RemovedTracksHandler(IDatabaseRepository database, IEnumerable<ISourceRepository> spurces, ILogger<RemovedTracksHandler> logger)
    {
        _database = database;
        _sources = spurces;
        _logger = logger;
    }

    public async Task Run()
    {
        _logger.LogInformation("Started processing removed tracks");

        foreach (var repository in _sources)
        {
            await RemoveTracks(repository, repository.Source);
        }

        _logger.LogInformation("Finished processing removed tracks");
    }

    private async Task RemoveTracks(ISourceRepository repository, LibrarySource source)
    {
        var removedTracks = await GetRemovedTracks(repository, source);

        _logger.LogInformation($"{removedTracks.Count} tracks to remove");

        if (removedTracks.Any())
        {
            await RemoveTracks(source, removedTracks);
        }
    }

    private async Task<List<string>> GetRemovedTracks(ISourceRepository repository, LibrarySource source)
    {
        var dbTracks = await _database.GetAllTracks(source);
        var sourceTracks = await repository.GetAllTracks();
        return dbTracks.Except(sourceTracks).ToList();
    }

    private async Task RemoveTracks(LibrarySource source, List<string> trackIds)
    {
        await _database.RemoveTracks(source, trackIds);

        _logger.LogInformation("Tracks removed");
    }
}