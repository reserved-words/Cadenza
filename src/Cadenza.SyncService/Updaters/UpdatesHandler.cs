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

    private async Task ProcessArtistUpdates(ISourceRepository repository, LibrarySource source, List<ItemUpdates> updates)
    {
        _logger.LogInformation($"{updates.Count} artist updates to process");

        foreach (var update in updates)
        {
            var tracks = await _database.GetTracksByArtist(source, update.Id);
            await TryUpdateTracks(repository, tracks, source, update);
        }

        _logger.LogInformation("All artist updates processed");
    }

    private async Task ProcessAlbumUpdates(ISourceRepository repository, LibrarySource source, List<ItemUpdates> updates)
    {
        _logger.LogInformation($"{updates.Count} album updates to process");

        foreach (var update in updates)
        {
            var tracks = await _database.GetTracksByAlbum(source, update.Id);
            await TryUpdateTracks(repository, tracks, source, update);
        }

        _logger.LogInformation("All album updates processed");
    }

    private async Task ProcessTrackUpdates(ISourceRepository repository, LibrarySource source, List<ItemUpdates> updates)
    {
        _logger.LogInformation($"{updates.Count} track updates to process");

        foreach (var update in updates)
        {
            var tracks = new List<string> { update.Id };
            await TryUpdateTracks(repository, tracks, source, update);
        }

        _logger.LogInformation("All track updates processed");
    }

    private async Task TryUpdateTracks(ISourceRepository repository, List<string> tracks, LibrarySource source, ItemUpdates update)
    {
        _logger.LogInformation($"Started processing update ID {update.Id}");

        try
        {
            await repository.UpdateTracks(tracks, update.Updates);
            await MarkUpdated(source, update);
            _logger.LogInformation($"Finished processing update ID {update.Id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to process update ID {update.Id}");
            await MarkErrored(source, update);
        }
    }

    private async Task MarkErrored(LibrarySource source, ItemUpdates update)
    {
        await _database.MarkErrored(source, update);
    }

    private async Task MarkUpdated(LibrarySource source, ItemUpdates update)
    {
        await _database.MarkUpdated(source, update);
    }
}