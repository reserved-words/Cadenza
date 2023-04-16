using Cadenza.API.SqlLibrary.Interfaces;
using Cadenza.API.SqlLibrary.Model;

namespace Cadenza.API.SqlLibrary.Services;

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

    public async Task MarkUpdateDone(ItemUpdateRequest request)
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

    public async Task MarkUpdateErrored(ItemUpdateRequest request)
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

    public async Task QueueRemoval(TrackRemovalRequest request)
    {
        var data = new NewTrackRemovalData
        {
            TrackIdFromSource = request.TrackId
        };

        await _insertionService.AddTrackRemoval(data);
    }

    public async Task QueueUpdates(ItemUpdateRequest request, LibrarySource source)
    {
        Func<ItemUpdateRequest, LibrarySource, PropertyUpdate, Task> queue = request.Type switch
        {
            LibraryItemType.Artist => QueueArtistUpdate,
            LibraryItemType.Album => QueueAlbumUpdate,
            LibraryItemType.Track => QueueTrackUpdate,
            _ => throw new NotImplementedException()
        };

        foreach (var update in request.Updates)
        {
            await queue(request, source, update);
        }
    }

    private async Task QueueArtistUpdate(ItemUpdateRequest request, LibrarySource source, PropertyUpdate update)
    {
        var artistUpdate = new NewArtistUpdateData
        {
            ArtistNameId = request.Id,
            SourceId = (int)source,
            PropertyName = update.Property.ToString(),
            OriginalValue = update.OriginalValue,
            UpdatedValue = update.UpdatedValue
        };

        await _insertionService.AddArtistUpdate(artistUpdate);
    }

    private async Task QueueAlbumUpdate(ItemUpdateRequest request, LibrarySource source, PropertyUpdate update)
    {
        var albumUpdate = new NewAlbumUpdateData
        {
            AlbumId = int.Parse(request.Id),
            SourceId = (int)source,
            PropertyName = update.Property.ToString(),
            OriginalValue = update.OriginalValue,
            UpdatedValue = update.UpdatedValue
        };

        await _insertionService.AddAlbumUpdate(albumUpdate);
    }

    private async Task QueueTrackUpdate(ItemUpdateRequest request, LibrarySource source, PropertyUpdate update)
    {
        var trackUpdate = new NewTrackUpdateData
        {
            TrackIdFromSource = request.Id,
            SourceId = (int)source,
            PropertyName = update.Property.ToString(),
            OriginalValue = update.OriginalValue,
            UpdatedValue = update.UpdatedValue
        };

        await _insertionService.AddTrackUpdate(trackUpdate);
    }
}