namespace Cadenza.Database.SqlLibrary.Services;

internal class QueueUpdater : IQueueUpdater
{
    private readonly IDataInsertService _insertionService;
    private readonly IDataUpdateService _updateService;

    public QueueUpdater(IDataInsertService insertionService, IDataUpdateService updateService)
    {
        _insertionService = insertionService;
        _updateService = updateService;
    }

    public async Task MarkRemovalDone(int requestId)
    {
        await _updateService.MarkRemovalDone(requestId);
    }

    public async Task MarkRemovalErrored(int requestId)
    {
        await _updateService.MarkRemovalErrored(requestId);
    }

    public async Task MarkUpdateDone(ItemUpdateRequestDTO request)
    {
        Func<int, Task> markAsDone = request.Type switch
        {
            LibraryItemType.Artist => _updateService.MarkArtistUpdateDone,
            LibraryItemType.Album => _updateService.MarkAlbumUpdateDone,
            LibraryItemType.Track => _updateService.MarkTrackUpdateDone,
            _ => throw new NotImplementedException()
        };

        foreach (var update in request.Updates)
        {
            await markAsDone(update.Id);
        }
    }

    public async Task MarkUpdateErrored(ItemUpdateRequestDTO request)
    {
        Func<int, Task> markAsErrored = request.Type switch
        {
            LibraryItemType.Artist => _updateService.MarkArtistUpdateErrored,
            LibraryItemType.Album => _updateService.MarkAlbumUpdateErrored,
            LibraryItemType.Track => _updateService.MarkTrackUpdateErrored,
            _ => throw new NotImplementedException()
        };

        foreach (var update in request.Updates)
        {
            await markAsErrored(update.Id);
        }
    }

    public async Task QueueRemoval(int trackId)
    {
        await _insertionService.AddTrackRemoval(trackId);
    }

    public async Task QueueUpdates(ItemUpdateRequestDTO request)
    {
        Func<ItemUpdateRequestDTO, PropertyUpdateDTO, Task> queue = request.Type switch
        {
            LibraryItemType.Artist => QueueArtistUpdate,
            LibraryItemType.Album => QueueAlbumUpdate,
            LibraryItemType.Track => QueueTrackUpdate,
            _ => throw new NotImplementedException()
        };

        foreach (var update in request.Updates)
        {
            await queue(request, update);
        }
    }

    private async Task QueueArtistUpdate(ItemUpdateRequestDTO request, PropertyUpdateDTO update)
    {
        var artistUpdate = new NewArtistUpdateData
        {
            ArtistId = request.Id,
            PropertyName = update.Property.ToString(),
            OriginalValue = update.OriginalValue,
            UpdatedValue = update.UpdatedValue
        };

        await _insertionService.AddArtistUpdate(artistUpdate);
    }

    private async Task QueueAlbumUpdate(ItemUpdateRequestDTO request, PropertyUpdateDTO update)
    {
        var albumUpdate = new NewAlbumUpdateData
        {
            AlbumId = request.Id,
            PropertyName = update.Property.ToString(),
            OriginalValue = update.OriginalValue,
            UpdatedValue = update.UpdatedValue
        };

        await _insertionService.AddAlbumUpdate(albumUpdate);
    }

    private async Task QueueTrackUpdate(ItemUpdateRequestDTO request, PropertyUpdateDTO update)
    {
        var trackUpdate = new NewTrackUpdateData
        {
            TrackId = request.Id,
            PropertyName = update.Property.ToString(),
            OriginalValue = update.OriginalValue,
            UpdatedValue = update.UpdatedValue
        };

        await _insertionService.AddTrackUpdate(trackUpdate);
    }
}