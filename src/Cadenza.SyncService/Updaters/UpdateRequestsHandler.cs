using Cadenza.Database.Interfaces;

namespace Cadenza.SyncService.Updaters;

internal class UpdateRequestsHandler : IService
{
    private readonly IMusicRepository _musicRepository;
    private readonly IQueueRepository _queueRepository;
    private readonly ILogger<UpdateRequestsHandler> _logger;
    private readonly IEnumerable<ISourceRepository> _sources;

    public UpdateRequestsHandler(IMusicRepository musicRepository, IQueueRepository queueRepository, IEnumerable<ISourceRepository> spurces, ILogger<UpdateRequestsHandler> logger)
    {
        _musicRepository = musicRepository;
        _queueRepository = queueRepository;
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
        var requests = await _queueRepository.GetUpdateRequests(source);

        await ProcessTrackUpdates(repository, source, requests.Where(u => u.Type == LibraryItemType.Track).ToList());
        await ProcessAlbumUpdates(repository, source, requests.Where(u => u.Type == LibraryItemType.Album).ToList());
        await ProcessArtistUpdates(repository, source, requests.Where(u => u.Type == LibraryItemType.Artist).ToList());
    }

    private async Task ProcessArtistUpdates(ISourceRepository repository, LibrarySource source, List<ItemUpdateRequestDTO> requests)
    {
        _logger.LogInformation($"{requests.Count} artist update requests to process");

        foreach (var request in requests)
        {
            var tracks = await _musicRepository.GetArtistTrackSourceIds(request.Id);
            await TryUpdateTracks(repository, tracks, source, request);
        }

        _logger.LogInformation("All artist update requests processed");
    }

    private async Task ProcessAlbumUpdates(ISourceRepository repository, LibrarySource source, List<ItemUpdateRequestDTO> requests)
    {
        _logger.LogInformation($"{requests.Count} album update requests to process");

        foreach (var request in requests)
        {
            var tracks = await _musicRepository.GetAlbumTrackSourceIds(request.Id);
            await TryUpdateTracks(repository, tracks, source, request);
        }

        _logger.LogInformation("All album update requests processed");
    }

    private async Task ProcessTrackUpdates(ISourceRepository repository, LibrarySource source, List<ItemUpdateRequestDTO> requests)
    {
        _logger.LogInformation($"{requests.Count} track update requests to process");

        foreach (var request in requests)
        {
            var trackIdFromSource = await _musicRepository.GetTrackIdFromSource(request.Id);
            var tracks = new List<string> { trackIdFromSource };
            await TryUpdateTracks(repository, tracks, source, request);
        }

        _logger.LogInformation("All track update requests processed");
    }

    private async Task TryUpdateTracks(ISourceRepository repository, List<string> tracks, LibrarySource source, ItemUpdateRequestDTO request)
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

    private async Task MarkErrored(ItemUpdateRequestDTO request)
    {
        await _queueRepository.MarkUpdateErrored(request);
    }

    private async Task MarkUpdated(ItemUpdateRequestDTO request)
    {
        await _queueRepository.MarkUpdateDone(request);
    }
}