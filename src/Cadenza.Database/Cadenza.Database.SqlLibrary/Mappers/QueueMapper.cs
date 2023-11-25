using Cadenza.Database.SqlLibrary.Mappers.Interfaces;
using Cadenza.Database.SqlLibrary.Model.Queue;

namespace Cadenza.Database.SqlLibrary.Mappers;

internal class QueueMapper : IQueueMapper
{
    public AddArtistUpdateParameter MapArtistUpdate(ItemUpdateRequestDTO request, PropertyUpdateDTO update)
    {
        return new AddArtistUpdateParameter
        {
            ArtistId = request.Id,
            PropertyName = update.Property.ToString(),
            OriginalValue = update.OriginalValue,
            UpdatedValue = update.UpdatedValue
        };
    }

    public AddAlbumUpdateParameter MapAlbumUpdate(ItemUpdateRequestDTO request, PropertyUpdateDTO update)
    {
        return new AddAlbumUpdateParameter
        {
            AlbumId = request.Id,
            PropertyName = update.Property.ToString(),
            OriginalValue = update.OriginalValue,
            UpdatedValue = update.UpdatedValue
        };
    }

    public AddTrackUpdateParameter MapTrackUpdate(ItemUpdateRequestDTO request, PropertyUpdateDTO update)
    {
        return new AddTrackUpdateParameter
        {
            TrackId = request.Id,
            PropertyName = update.Property.ToString(),
            OriginalValue = update.OriginalValue,
            UpdatedValue = update.UpdatedValue
        };
    }

    public List<ItemUpdateRequestDTO> MapAlbumUpdateRequests(List<GetAlbumUpdatesResult> data)
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

    public List<ItemUpdateRequestDTO> MapArtistUpdateRequests(List<GetArtistUpdatesResult> data)
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

    public List<ItemUpdateRequestDTO> MapTrackUpdateRequests(List<GetTrackUpdatesResult> data)
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

    public SyncTrackRemovalRequestDTO MapSyncTrackRemovalRequest(GetTrackRemovalsResult data)
    {
        return new SyncTrackRemovalRequestDTO
        {
            RequestId = data.RequestId,
            TrackIdFromSource = data.TrackIdFromSource
        };
    }
}
