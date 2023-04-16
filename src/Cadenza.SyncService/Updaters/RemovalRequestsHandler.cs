namespace Cadenza.SyncService.Updaters;

internal class RemovalRequestsHandler : IService
{
    private readonly IDatabaseRepository _database;
    private readonly ILogger<RemovalRequestsHandler> _logger;
    private readonly IEnumerable<ISourceRepository> _sources;

    public RemovalRequestsHandler(IDatabaseRepository database, IEnumerable<ISourceRepository> spurces, ILogger<RemovalRequestsHandler> logger)
    {
        _database = database;
        _sources = spurces;
        _logger = logger;
    }

    public async Task Run()
    {
        _logger.LogInformation("Started processing removal requests");

        foreach (var repository in _sources)
        {
            await ProcessUpdates(repository, repository.Source);
        }

        _logger.LogInformation("Finished processing removal requests");
    }

    private async Task ProcessUpdates(ISourceRepository repository, LibrarySource source)
    {
        var requests = await _database.GetRemovalRequests(source);

        _logger.LogInformation($"{requests.Count} track removal requests to process");

        foreach (var request in requests)
        {
            await TryRemoveTrack(repository, request);
        }

        _logger.LogInformation("All track removal requests processed");
    }

    private async Task TryRemoveTrack(ISourceRepository repository, TrackRemovalRequest request)
    {
        _logger.LogInformation($"Started processing removal request ID {request.RequestId}");

        try
        {
            await repository.RemoveTrack(request);
            await MarkDone(request);
            _logger.LogInformation($"Finished processing removal request ID {request.RequestId}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to process removal request ID {request.RequestId}");
            await MarkErrored(request);
        }
    }

    private async Task MarkErrored(TrackRemovalRequest request)
    {
        await _database.MarkRemovalErrored(request);
    }

    private async Task MarkDone(TrackRemovalRequest request)
    {
        await _database.MarkRemovalDone(request);
    }
}