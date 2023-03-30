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

    public async Task MarkUpdatesDone(ItemUpdates updates)
    {
        Func<int, Task> markAsDone = updates.Type switch
        {
            LibraryItemType.Artist => _updateService.MarkArtistUpdateDone,
            LibraryItemType.Album => _updateService.MarkAlbumUpdateDone,
            LibraryItemType.Track => _updateService.MarkTrackUpdateDone,
            _ => throw new NotImplementedException()
        };

        foreach (var update in updates.Updates)
        {
            await markAsDone(update.Id);
        }
    }

    public async Task QueueUpdates(ItemUpdates updates, LibrarySource source)
    {
        Func<ItemUpdates, LibrarySource, PropertyUpdate, Task> queue = updates.Type switch
        {
            LibraryItemType.Artist => QueueArtistUpdate,
            LibraryItemType.Album => QueueAlbumUpdate,
            LibraryItemType.Track => QueueTrackUpdate,
            _ => throw new NotImplementedException()
        };

        foreach (var update in updates.Updates)
        {
            await queue(updates, source, update);
        }
    }

    private async Task QueueArtistUpdate(ItemUpdates updates, LibrarySource source, PropertyUpdate update)
    {
        var artistUpdate = new NewArtistUpdateData
        {
            ArtistNameId = updates.Id,
            Name = updates.Name,
            SourceId = (int)source,
            PropertyName = update.Property.ToString(),
            OriginalValue = update.OriginalValue,
            UpdatedValue = update.UpdatedValue
        };

        await _insertionService.AddArtistUpdate(artistUpdate);
    }

    private async Task QueueAlbumUpdate(ItemUpdates updates, LibrarySource source, PropertyUpdate update)
    {
        var albumUpdate = new NewAlbumUpdateData
        {
            AlbumId = int.Parse(updates.Id),
            Name = updates.Name,
            SourceId = (int)source,
            PropertyName = update.Property.ToString(),
            OriginalValue = update.OriginalValue,
            UpdatedValue = update.UpdatedValue
        };

        await _insertionService.AddAlbumUpdate(albumUpdate);
    }

    private async Task QueueTrackUpdate(ItemUpdates updates, LibrarySource source, PropertyUpdate update)
    {
        var trackUpdate = new NewTrackUpdateData
        {
            TrackIdFromSource = updates.Id,
            Name = updates.Name,
            SourceId = (int)source,
            PropertyName = update.Property.ToString(),
            OriginalValue = update.OriginalValue,
            UpdatedValue = update.UpdatedValue
        };

        await _insertionService.AddTrackUpdate(trackUpdate);
    }
}