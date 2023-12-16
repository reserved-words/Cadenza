using Cadenza.Database.SqlLibrary.Mappers.Interfaces;
using Cadenza.Database.SqlLibrary.Model.Queue;

namespace Cadenza.Database.SqlLibrary.Mappers;

internal class QueueMapper : IQueueMapper
{
    private readonly IImageConverter _imageConverter;

    public QueueMapper(IImageConverter imageConverter)
    {
        _imageConverter = imageConverter;
    }

    public List<AlbumUpdateSyncDTO> MapAlbumUpdates(List<GetAlbumUpdatesResult> data)
    {


        return data.Select(a => new AlbumUpdateSyncDTO
        {
            AlbumId = a.AlbumId,
            Title = a.Title,
            ReleaseType = a.ReleaseType,
            Year = a.Year,
            DiscCount = a.DiscCount,
            ArtworkBase64 = a.ArtworkContent == null
                ? null
                : _imageConverter.GetBase64UrlFromImage(new ArtworkImage(a.ArtworkContent, a.ArtworkMimeType)),
            TagList = a.TagList
        })
        .ToList();
    }

    public List<ArtistUpdateSyncDTO> MapArtistUpdates(List<GetArtistUpdatesResult> data)
    {
        return data.Select(a => new ArtistUpdateSyncDTO
        {
            ArtistId = a.ArtistId,
            Name = a.Name,
            Grouping = a.Grouping,
            Genre = a.Genre,
            City = a.City,
            State = a.State,
            Country = a.Country,
            ImageBase64 = a.ImageContent == null
                 ? null
                 : _imageConverter.GetBase64UrlFromImage(new ArtworkImage(a.ImageContent, a.ImageMimeType)),
            TagList = a.TagList
        })
        .ToList();
    }

    public List<TrackUpdateSyncDTO> MapTrackUpdates(List<GetTrackUpdatesResult> data)
    {
        return data.Select(t => new TrackUpdateSyncDTO
        {
            TrackId = t.TrackId,
            Title = t.Title,
            Year = t.Year,
            Lyrics = t.Lyrics,
            DiscNo = t.DiscNo,
            TrackNo = t.TrackNo,
            DiscTrackCount = t.DiscTrackCount,
            TagList = t.TagList
        })
        .ToList();
    }

    public TrackRemovalSyncDTO MapSyncTrackRemovalRequest(GetTrackRemovalsResult data)
    {
        return new TrackRemovalSyncDTO
        {
            RequestId = data.RequestId,
            TrackIdFromSource = data.TrackIdFromSource
        };
    }
}
