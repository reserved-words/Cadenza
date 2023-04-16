using Cadenza.API.SqlLibrary.Interfaces;
using Cadenza.API.SqlLibrary.Model;

namespace Cadenza.API.SqlLibrary.Services;

internal class QueueReader : IQueueReader
{
    private readonly IDataReadService _readService;

    public QueueReader(IDataReadService readService)
    {
        _readService = readService;
    }

    public async Task<List<SyncTrackRemovalRequest>> GetRemovalRequests(LibrarySource source)
    {
        var requests = await _readService.GetTrackRemovals(source);
        return requests.Select(r => new SyncTrackRemovalRequest
        {
            RequestId = r.RequestId,
            TrackIdFromSource = r.TrackIdFromSource
        })
        .ToList();
    }

    public async Task<List<ItemUpdateRequest>> GetUpdateRequests(LibrarySource source)
    {
        var artistUpdates = await _readService.GetArtistUpdates(source);
        var albumUpdates = await _readService.GetAlbumUpdates(source);
        var trackUpdates = await _readService.GetTrackUpdates(source);

        return ConvertArtistUpdateRequests(artistUpdates)
            .Concat(ConvertAlbumUpdateRequests(albumUpdates))
            .Concat(ConvertTrackUpdateRequests(trackUpdates))
            .ToList();
    }

    private List<ItemUpdateRequest> ConvertAlbumUpdateRequests(List<AlbumUpdateData> data)
    {
        return data.GroupBy(d => d.AlbumId)
            .Select(a => new ItemUpdateRequest
            {
                Type = LibraryItemType.Album,
                Id = a.Key,
                Updates = a.Select(u => new PropertyUpdate
                {
                    Id = u.Id,
                    Property = Enum.Parse<ItemProperty>(u.PropertyName),
                    OriginalValue = u.OriginalValue,
                    UpdatedValue = u.UpdatedValue
                }).ToList()
            })
            .ToList();
    }

    private List<ItemUpdateRequest> ConvertArtistUpdateRequests(List<ArtistUpdateData> data)
    {
        return data.GroupBy(d => d.ArtistId)
            .Select(a => new ItemUpdateRequest
            {
                Type = LibraryItemType.Artist,
                Id = a.Key,
                Updates = a.Select(u => new PropertyUpdate
                {
                    Id = u.Id,
                    Property = Enum.Parse<ItemProperty>(u.PropertyName),
                    OriginalValue = u.OriginalValue,
                    UpdatedValue = u.UpdatedValue
                }).ToList()
            })
            .ToList();
    }

    private List<ItemUpdateRequest> ConvertTrackUpdateRequests(List<TrackUpdateData> data)
    {
        return data.GroupBy(d => d.TrackId)
            .Select(a => new ItemUpdateRequest
            {
                Type = LibraryItemType.Track,
                Id = a.Key,
                Updates = a.Select(u => new PropertyUpdate
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
