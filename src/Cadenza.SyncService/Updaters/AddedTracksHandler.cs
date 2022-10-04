namespace Cadenza.SyncService.Updaters;

internal class AddedTracksHandler : IService
{
    private readonly IDatabaseRepository _database;
    private readonly ILogger<AddedTracksHandler> _logger;
    private readonly IEnumerable<ISourceRepository> _sources;

    public AddedTracksHandler(IDatabaseRepository database, IEnumerable<ISourceRepository> spurces, ILogger<AddedTracksHandler> logger)
    {
        _database = database;
        _sources = spurces;
        _logger = logger;
    }

    public async Task Run()
    {
        _logger.LogInformation("Started processing added tracks");

        foreach (var repository in _sources)
        {
            await AddTracks(repository, repository.Source);
        }

        _logger.LogInformation("Finished processing added tracks");
    }

    private async Task AddTracks(ISourceRepository repository, LibrarySource source)
    {
        var addedTracks = await GetAddedTracks(repository, source);

        _logger.LogInformation($"{addedTracks.Count} tracks to add");

        foreach (var trackId in addedTracks)
        {
            _logger.LogInformation($"Started adding track {trackId}");

            await AddTrack(repository, source, trackId);

            _logger.LogInformation($"Finished adding track {trackId}");
        }
    }

    private async Task AddTrack(ISourceRepository repository, LibrarySource source, string trackId)
    {
        var track = await repository.GetTrack(trackId);

        _logger.LogInformation($"Adding track {track.Track.Title} by {track.Artist.Name}");

        await _database.AddTrack(source, track);
    }

    private async Task<List<string>> GetAddedTracks(ISourceRepository repository, LibrarySource source)
    {
        var dbTracks = await _database.GetAllTracks(source);
        var sourceTracks = await repository.GetAllTracks();
        return sourceTracks.Except(dbTracks).ToList();
    }
}
