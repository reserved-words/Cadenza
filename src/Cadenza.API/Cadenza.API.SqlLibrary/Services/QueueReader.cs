using Cadenza.API.SqlLibrary.Interfaces;
using Cadenza.API.SqlLibrary.Model;
using Cadenza.Common.Enums;

namespace Cadenza.API.SqlLibrary.Services;

internal class QueueReader : IQueueReader
{
    private readonly IDataReadService _readService;

    public QueueReader(IDataReadService readService)
    {
        _readService = readService;
    }

    public async Task<List<SyncTrackRemovalRequestDTO>> GetRemovalRequests(LibrarySource source)
    {
        var requests = await _readService.GetTrackRemovals(source);
        return requests.Select(r => new SyncTrackRemovalRequestDTO
        {
            RequestId = r.RequestId,
            TrackIdFromSource = r.TrackIdFromSource
        })
        .ToList();
    }

    public async Task<List<ItemUpdateRequestDTO>> GetUpdateRequests(LibrarySource source)
    {
        var artistUpdates = await _readService.GetArtistUpdates(source);
        var albumUpdates = await _readService.GetAlbumUpdates(source);
        var trackUpdates = await _readService.GetTrackUpdates(source);

        return ConvertArtistUpdateRequests(artistUpdates)
            .Concat(ConvertAlbumUpdateRequests(albumUpdates))
            .Concat(ConvertTrackUpdateRequests(trackUpdates))
            .ToList();
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
