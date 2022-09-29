using Cadenza.Common.Domain.Model.Album;

namespace Cadenza.API.Database.Services.Converters;

internal class AlbumTrackConverter : IAlbumTrackConverter
{
    public AlbumTrackLink ToModel(JsonAlbumTrack link)
    {
        return new AlbumTrackLink
        {
            TrackId = link.TrackId,
            AlbumId = link.AlbumId,
            Position = new AlbumTrackPosition(link.DiscNo ?? 1, link.TrackNo)
        };
    }

    public JsonAlbumTrack ToJson(AlbumTrackLink link)
    {
        return new JsonAlbumTrack
        {
            TrackId = link.TrackId,
            AlbumId = link.AlbumId,
            DiscNo = link.Position.DiscNo == 1
                    ? null
                    : link.Position.DiscNo,
            TrackNo = link.Position.TrackNo
        };
    }
}