using Cadenza.Database.Interfaces;

namespace Cadenza.SyncService.Updaters;

internal class UpdatesHandler : IService
{
    private readonly ILibraryRepository _musicRepository;
    private readonly IQueueRepository _queueRepository;
    private readonly ILogger<UpdatesHandler> _logger;
    private readonly IEnumerable<ISourceRepository> _sources;

    public UpdatesHandler(ILibraryRepository musicRepository, IQueueRepository queueRepository, IEnumerable<ISourceRepository> spurces, ILogger<UpdatesHandler> logger)
    {
        _musicRepository = musicRepository;
        _queueRepository = queueRepository;
        _sources = spurces;
        _logger = logger;
    }

    public async Task Run()
    {
        _logger.LogDebug("Started processing update requests");

        foreach (var repository in _sources)
        {
            await ProcessUpdates(repository, repository.Source);
        }

        _logger.LogDebug("Finished processing update requests");
    }

    private async Task ProcessUpdates(ISourceRepository repository, LibrarySource source)
    {
        var trackRequests = await _queueRepository.GetTrackUpdateRequests(source);
        await ProcessTrackUpdates(repository, trackRequests);

        var albumRequests = await _queueRepository.GetAlbumUpdateRequests(source);
        await ProcessAlbumUpdates(repository, albumRequests);

        var artistRequests = await _queueRepository.GetArtistUpdateRequests(source);
        await ProcessArtistUpdates(repository, artistRequests);
    }

    private async Task ProcessArtistUpdates(ISourceRepository repository, List<ArtistUpdateSyncDTO> requests)
    {
        _logger.LogDebug($"{requests.Count} artist update requests to process");

        foreach (var request in requests)
        {
            var tracks = await _musicRepository.GetArtistTrackSourceIds(request.ArtistId);

            var propertyUpdates = new List<PropertyUpdateDTO>
            {
                new () { Property = ItemProperty.ArtistGrouping, NewValue = request.Grouping },
                new () { Property = ItemProperty.ArtistGenre, NewValue = request.Genre },
                new () { Property = ItemProperty.ArtistCountry, NewValue = request.Country },
                new () { Property = ItemProperty.ArtistState, NewValue = request.State },
                new () { Property = ItemProperty.ArtistCity, NewValue = request.City },
                new () { Property = ItemProperty.ArtistImage, NewValue = request.ImageBase64 },
                new () { Property = ItemProperty.ArtistTags, NewValue = request.TagList }
            };

            await TryUpdateTracks(repository, LibraryItemType.Artist, request.ArtistId, tracks, propertyUpdates);
        }

        _logger.LogDebug("All artist update requests processed");
    }

    private async Task ProcessAlbumUpdates(ISourceRepository repository, List<AlbumUpdateSyncDTO> requests)
    {
        _logger.LogDebug($"{requests.Count} album update requests to process");

        foreach (var request in requests)
        {
            var tracks = await _musicRepository.GetAlbumTrackSourceIds(request.AlbumId);

            var propertyUpdates = new List<PropertyUpdateDTO>
            {
                new () { Property = ItemProperty.AlbumTitle, NewValue = request.Title },
                new () { Property = ItemProperty.AlbumReleaseType, NewValue = request.ReleaseType },
                new () { Property = ItemProperty.AlbumReleaseYear, NewValue = request.Year },
                new () { Property = ItemProperty.AlbumDiscCount, NewValue = request.DiscCount },
                new () { Property = ItemProperty.AlbumArtwork, NewValue = request.ArtworkBase64 },
                new () { Property = ItemProperty.AlbumTags, NewValue = request.TagList }
            };

            await TryUpdateTracks(repository, LibraryItemType.Album, request.AlbumId, tracks, propertyUpdates);
        }

        _logger.LogDebug("All album update requests processed");
    }

    private async Task ProcessTrackUpdates(ISourceRepository repository, List<TrackUpdateSyncDTO> requests)
    {
        _logger.LogDebug($"{requests.Count} track update requests to process");

        foreach (var request in requests)
        {
            var trackIdFromSource = await _musicRepository.GetTrackIdFromSource(request.TrackId);
            var tracks = new List<string> { trackIdFromSource };

            var propertyUpdates = new List<PropertyUpdateDTO>
            {
                new () { Property = ItemProperty.TrackTitle, NewValue = request.Title },
                new () { Property = ItemProperty.TrackYear, NewValue = request.Year },
                new () { Property = ItemProperty.TrackLyrics, NewValue = request.Lyrics },
                new () { Property = ItemProperty.TrackDiscNo, NewValue = request.DiscNo },
                new () { Property = ItemProperty.TrackNo, NewValue = request.TrackNo },
                new () { Property = ItemProperty.TrackDiscTrackCount, NewValue = request.DiscTrackCount },
                new () { Property = ItemProperty.TrackTags, NewValue = request.TagList }
            };

            await TryUpdateTracks(repository, LibraryItemType.Track, request.TrackId, tracks, propertyUpdates);
        }

        _logger.LogDebug("All track update requests processed");
    }

    private async Task TryUpdateTracks(ISourceRepository repository, LibraryItemType itemType, int id, List<string> tracks, List<PropertyUpdateDTO> propertyUpdates)
    {
        _logger.LogInformation($"Started processing {itemType} update ID {id}");

        try
        {
            await repository.UpdateTracks(tracks, propertyUpdates);
            await MarkUpdated(itemType, id);
            _logger.LogInformation($"Finished processing {itemType} update ID {id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to process {itemType} update ID {id}");
            await MarkErrored(itemType, id);
        }
    }

    private async Task MarkErrored(LibraryItemType itemType, int id)
    {
        if (itemType == LibraryItemType.Track)
        {
            await _queueRepository.MarkTrackUpdateErrored(id);
        }
        else if (itemType == LibraryItemType.Album)
        {
            await _queueRepository.MarkAlbumUpdateErrored(id);
        }
        else if (itemType == LibraryItemType.Artist)
        {
            await _queueRepository.MarkArtistUpdateErrored(id);
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    private async Task MarkUpdated(LibraryItemType itemType, int id)
    {
        if (itemType == LibraryItemType.Track)
        {
            await _queueRepository.MarkTrackUpdateDone(id);
        }
        else if (itemType == LibraryItemType.Album)
        {
            await _queueRepository.MarkAlbumUpdateDone(id);
        }
        else if (itemType == LibraryItemType.Artist)
        {
            await _queueRepository.MarkArtistUpdateDone(id);
        }
        else
        {
            throw new NotImplementedException();
        }
    }
}