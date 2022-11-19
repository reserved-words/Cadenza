namespace Cadenza.SyncService.Updaters;

internal class UpdatesHandler : IService
{
    private readonly IDatabaseRepository _database;
    private readonly ILogger<UpdatesHandler> _logger;
    private readonly IEnumerable<ISourceRepository> _sources;

    public UpdatesHandler(IDatabaseRepository database, IEnumerable<ISourceRepository> spurces, ILogger<UpdatesHandler> logger)
    {
        _database = database;
        _sources = spurces;
        _logger = logger;
    }

    public async Task Run()
    {
        _logger.LogInformation("Started processing updated tracks");

        foreach (var repository in _sources)
        {
            await ProcessUpdates(repository, repository.Source);
        }

        _logger.LogInformation("Finished processing updated tracks");
    }

    private async Task ProcessUpdates(ISourceRepository repository, LibrarySource source)
    {
        var updates = await _database.GetUpdates(source);

        await ProcessTrackUpdates(repository, source, updates.Where(u => u.Type == LibraryItemType.Track).ToList());
        await ProcessAlbumUpdates(repository, source, updates.Where(u => u.Type == LibraryItemType.Album).ToList());
        await ProcessArtistUpdates(repository, source, updates.Where(u => u.Type == LibraryItemType.Artist).ToList());
    }

    private async Task ProcessArtistUpdates(ISourceRepository repository, LibrarySource source, List<EditedItem> updates)
    {
		_logger.LogInformation($"{updates.Count} artist updates to process");

        foreach (var update in updates)
        {
			_logger.LogInformation($"Started processing update ID {update.Id}");
            var tracks = await _database.GetTracksByArtist(source, update.Id);
            await repository.UpdateTracks(tracks, update.Properties);
            await MarkUpdated(source, update);
            _logger.LogInformation($"Finished processing update ID {update.Id}");
        }

        _logger.LogInformation("All artist updates processed");
    }

    private async Task ProcessAlbumUpdates(ISourceRepository repository, LibrarySource source, List<EditedItem> updates)
    {
		_logger.LogInformation($"{updates.Count} album updates to process");

        foreach (var update in updates)
        {
            _logger.LogInformation($"Started processing update ID {update.Id}");
            var tracks = await _database.GetTracksByAlbum(source, update.Id);
            await repository.UpdateTracks(tracks, update.Properties);
            await MarkUpdated(source, update);
            _logger.LogInformation($"Finished processing update ID {update.Id}");
        }

        _logger.LogInformation("All album updates processed");
    }

    private async Task ProcessTrackUpdates(ISourceRepository repository, LibrarySource source, List<EditedItem> updates)
    {
        _logger.LogInformation($"{updates.Count} track updates to process");

        foreach (var update in updates)
        {
            _logger.LogInformation($"Started processing update ID {update.Id}");
            await repository.UpdateTracks(new List<string> { update.Id }, update.Properties);
            await MarkUpdated(source, update);
            _logger.LogInformation($"Finished processing update ID {update.Id}");
        }

        _logger.LogInformation("All track updates processed");
    }

    private async Task MarkUpdated(LibrarySource source, EditedItem update)
    {
        await _database.MarkUpdated(source, update);
    }
}