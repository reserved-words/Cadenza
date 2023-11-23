using Cadenza.Database.Interfaces;

namespace Cadenza.SyncService.Updaters;

internal class SyncHandler : IService
{
    private readonly ILogger<UpdateRequestsHandler> _logger;
    private readonly IEnumerable<ISourceRepository> _sources;
    private readonly ILibraryRepository _libraryRepository;
    private readonly IQueueRepository _queueRepository;
    private readonly IUpdateRepository _updateRepository;

    public SyncHandler(ILibraryRepository musicRepository, IQueueRepository queueRepository, IEnumerable<ISourceRepository> spurces, ILogger<UpdateRequestsHandler> logger, IUpdateRepository updateRepository)
    {
        _sources = spurces;
        _logger = logger;
        _libraryRepository = musicRepository;
        _queueRepository = queueRepository;
        _updateRepository = updateRepository;
    }

    public async Task Run()
    {
        _logger.LogInformation("Started syncing repositories");

        foreach (var repository in _sources)
        {
            await ProcessRemovalRequests(repository);

            var dbTracks = await _libraryRepository.GetAllTracks(repository.Source);
            var sourceTracks = await repository.GetAllTracks();

            await RemoveDbTracksThatAreNotInSource(repository, dbTracks, sourceTracks);
            await AddDbTracksThatAreInSource(repository, dbTracks, sourceTracks);
        }

        _logger.LogInformation("Finished syncing repositories");
    }

    private async Task AddDbTracksThatAreInSource(ISourceRepository repository, List<string> dbTracks, List<string> sourceTracks)
    {
        var addedTracks = sourceTracks.Except(dbTracks).ToList();
        _logger.LogInformation($"{addedTracks.Count} tracks to add");

        foreach (var trackId in addedTracks)
        {
            _logger.LogInformation($"Adding track {trackId}");
            var track = await repository.GetTrack(trackId);
            await _updateRepository.AddTrack(repository.Source, track);
        }
    }

    private async Task RemoveDbTracksThatAreNotInSource(ISourceRepository repository, List<string> dbTracks, List<string> sourceTracks)
    {
        var removedTracks = dbTracks.Except(sourceTracks).ToList();
        _logger.LogInformation($"{removedTracks.Count} tracks to remove");

        if (removedTracks.Any())
        {
            await _updateRepository.RemoveTracks(removedTracks);
            _logger.LogInformation($"{removedTracks.Count} tracks removed");
        }
    }

    private async Task ProcessRemovalRequests(ISourceRepository repository)
    {
        var requests = await _queueRepository.GetRemovalRequests(repository.Source);

        foreach (var request in requests)
        {
            _logger.LogInformation($"Processing removal request for {request.TrackIdFromSource} ({request.RequestId})");

            try
            {
                await repository.RemoveTrack(request);
                await _queueRepository.MarkRemovalDone(request.RequestId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to process removal request for {request.TrackIdFromSource} ({request.RequestId})");
                await _queueRepository.MarkRemovalErrored(request.RequestId);
            }
        }
    }
}