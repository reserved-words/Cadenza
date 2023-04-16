namespace Cadenza.SyncService.Updaters;

internal class UpdateRequestsHandler : IService
{
    private readonly IDatabaseRepository _database;
    private readonly ILogger<UpdateRequestsHandler> _logger;
    private readonly IEnumerable<ISourceRepository> _sources;

    public UpdateRequestsHandler(IDatabaseRepository database, IEnumerable<ISourceRepository> spurces, ILogger<UpdateRequestsHandler> logger)
    {
        _database = database;
        _sources = spurces;
        _logger = logger;
    }

    public async Task Run()
    {
        _logger.LogInformation("Started processing update requests");

        foreach (var repository in _sources)
        {
            await ProcessUpdates(repository, repository.Source);
        }

        _logger.LogInformation("Finished processing update requests");
    }

    private async Task ProcessUpdates(ISourceRepository repository, LibrarySource source)
    {
        var requests = await _database.GetUpdateRequests(source);

        await ProcessTrackUpdates(repository, source, requests.Where(u => u.Type == LibraryItemType.Track).ToList());
        await ProcessAlbumUpdates(repository, source, requests.Where(u => u.Type == LibraryItemType.Album).ToList());
        await ProcessArtistUpdates(repository, source, requests.Where(u => u.Type == LibraryItemType.Artist).ToList());
    }

    private async Task ProcessArtistUpdates(ISourceRepository repository, LibrarySource source, List<ItemUpdateRequest> requests)
    {
        _logger.LogInformation($"{requests.Count} artist update requests to process");

        foreach (var request in requests)
        {
            var tracks = await _database.GetTracksByArtist(request.Id);
            await TryUpdateTracks(repository, tracks, source, request);
        }

        _logger.LogInformation("All artist update requests processed");
    }

    private async Task ProcessAlbumUpdates(ISourceRepository repository, LibrarySource source, List<ItemUpdateRequest> requests)
    {
        _logger.LogInformation($"{requests.Count} album update requests to process");

        foreach (var request in requests)
        {
            var tracks = await _database.GetTracksByAlbum(request.Id);
            await TryUpdateTracks(repository, tracks, source, request);
        }

        _logger.LogInformation("All album update requests processed");
    }

    private async Task ProcessTrackUpdates(ISourceRepository repository, LibrarySource source, List<ItemUpdateRequest> requests)
    {
        _logger.LogInformation($"{requests.Count} track update requests to process");

        foreach (var request in requests)
        {
            var trackIdFromSource = await _database.GetTrackIdFromSource(request.Id);
            var tracks = new List<string> { trackIdFromSource.IdFromSource };
            await TryUpdateTracks(repository, tracks, source, request);
        }

        _logger.LogInformation("All track update requests processed");
    }

    private async Task TryUpdateTracks(ISourceRepository repository, List<string> tracks, LibrarySource source, ItemUpdateRequest request)
    {
        _logger.LogInformation($"Started processing update ID {request.Id}");

        try
        {
            await repository.UpdateTracks(tracks, request.Updates);
            await MarkUpdated(request);
            _logger.LogInformation($"Finished processing update ID {request.Id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to process update ID {request.Id}");
            await MarkErrored(request);
        }
    }

    private async Task MarkErrored(ItemUpdateRequest request)
    {
        await _database.MarkUpdateErrored(request);
    }

    private async Task MarkUpdated(ItemUpdateRequest request)
    {
        await _database.MarkUpdateDone(request);
    }
}