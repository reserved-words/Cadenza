namespace Cadenza.Database.SqlLibrary.Repositories;

internal class QueueRepository : IQueueRepository
{
    private readonly IQueue _queue;

    public QueueRepository(IQueue queue)
    {
        _queue = queue;
    }

    public async Task AddRemovalRequest(int trackId)
    {
        await _queue.AddTrackRemoval(trackId);
    }

    public async Task AddUpdateRequest(ItemUpdateRequestDTO request)
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

    public async Task<List<SyncTrackRemovalRequestDTO>> GetRemovalRequests(LibrarySource source)
    {
        var requests = await _queue.GetTrackRemovals(source);
        return requests.Select(r => new SyncTrackRemovalRequestDTO
        {
            RequestId = r.RequestId,
            TrackIdFromSource = r.TrackIdFromSource
        })
        .ToList();
    }

    public async Task<List<ItemUpdateRequestDTO>> GetUpdateRequests(LibrarySource source)
    {
        var artistUpdates = await _queue.GetArtistUpdates(source);
        var albumUpdates = await _queue.GetAlbumUpdates(source);
        var trackUpdates = await _queue.GetTrackUpdates(source);

        return ConvertArtistUpdateRequests(artistUpdates)
            .Concat(ConvertAlbumUpdateRequests(albumUpdates))
            .Concat(ConvertTrackUpdateRequests(trackUpdates))
            .ToList();
    }

    public async Task MarkRemovalDone(int requestId)
    {
        await _queue.MarkTrackRemovalDone(requestId);
    }

    public async Task MarkRemovalErrored(int requestId)
    {
        await _queue.MarkTrackRemovalErrored(requestId);
    }

    public async Task MarkUpdateDone(ItemUpdateRequestDTO request)
    {
        Func<int, Task> markAsDone = request.Type switch
        {
            LibraryItemType.Artist => _queue.MarkArtistUpdateDone,
            LibraryItemType.Album => _queue.MarkAlbumUpdateDone,
            LibraryItemType.Track => _queue.MarkTrackUpdateDone,
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
            LibraryItemType.Artist => _queue.MarkArtistUpdateErrored,
            LibraryItemType.Album => _queue.MarkAlbumUpdateErrored,
            LibraryItemType.Track => _queue.MarkTrackUpdateErrored,
            _ => throw new NotImplementedException()
        };

        foreach (var update in request.Updates)
        {
            await markAsErrored(update.Id);
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

        await _queue.AddArtistUpdate(artistUpdate);
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

        await _queue.AddAlbumUpdate(albumUpdate);
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

        await _queue.AddTrackUpdate(trackUpdate);
    }

    private List<ItemUpdateRequestDTO> ConvertAlbumUpdateRequests(List<AlbumUpdateData> data)
    {
        return data.GroupBy(d => d.AlbumId)
            .Select(a => new ItemUpdateRequestDTO
            {
                Type = LibraryItemType.Album,
                Id = a.Key,
                Updates = a.Select(u => new PropertyUpdateDTO
                {
                    Id = u.Id,
                    Property = Enum.Parse<ItemProperty>(u.PropertyName),
                    OriginalValue = u.OriginalValue,
                    UpdatedValue = u.UpdatedValue
                }).ToList()
            })
            .ToList();
    }

    private List<ItemUpdateRequestDTO> ConvertArtistUpdateRequests(List<ArtistUpdateData> data)
    {
        return data.GroupBy(d => d.ArtistId)
            .Select(a => new ItemUpdateRequestDTO
            {
                Type = LibraryItemType.Artist,
                Id = a.Key,
                Updates = a.Select(u => new PropertyUpdateDTO
                {
                    Id = u.Id,
                    Property = Enum.Parse<ItemProperty>(u.PropertyName),
                    OriginalValue = u.OriginalValue,
                    UpdatedValue = u.UpdatedValue
                }).ToList()
            })
            .ToList();
    }

    private List<ItemUpdateRequestDTO> ConvertTrackUpdateRequests(List<TrackUpdateData> data)
    {
        return data.GroupBy(d => d.TrackId)
            .Select(a => new ItemUpdateRequestDTO
            {
                Type = LibraryItemType.Track,
                Id = a.Key,
                Updates = a.Select(u => new PropertyUpdateDTO
                {
                    Id = u.Id,
                    Property = Enum.Parse<ItemProperty>(u.PropertyName),
                    OriginalValue = u.OriginalValue,
                    UpdatedValue = u.UpdatedValue
                }).ToList()
            })
            .ToList();
    }
}
