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

    public async Task<List<ItemUpdates>> GetUpdates(LibrarySource source)
    {
        var artistUpdates = await _readService.GetArtistUpdates(source);
        var albumUpdates = await _readService.GetAlbumUpdates(source); 
        var trackUpdates = await _readService.GetTrackUpdates(source);

        return ConvertArtistUpdates(artistUpdates)
            .Concat(ConvertAlbumUpdates(albumUpdates))
            .Concat(ConvertTrackUpdates(trackUpdates))
            .ToList();
    }

    private List<ItemUpdates> ConvertAlbumUpdates(List<AlbumUpdateData> data)
    {
        return data.GroupBy(d => d.AlbumId)
            .Select(a => new ItemUpdates
            {
                Type = LibraryItemType.Album,
                Id = a.Key.ToString(),
                Name = a.First().Name,
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

    private List<ItemUpdates> ConvertArtistUpdates(List<ArtistUpdateData> data)
    {
        return data.GroupBy(d => d.NameId)
            .Select(a => new ItemUpdates
            {
                Type = LibraryItemType.Artist,
                Id = a.Key,
                Name = a.First().Name,
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

    private List<ItemUpdates> ConvertTrackUpdates(List<TrackUpdateData> data)
    {
        return data.GroupBy(d => d.IdFromSource)
            .Select(a => new ItemUpdates
            {
                Type = LibraryItemType.Track,
                Id = a.Key.ToString(),
                Name = a.First().Name,
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
