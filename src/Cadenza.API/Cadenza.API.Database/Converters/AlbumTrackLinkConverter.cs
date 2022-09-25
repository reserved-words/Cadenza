using Cadenza.API.Database.Interfaces.Converters;
using Cadenza.API.Database.Model;
using Cadenza.Domain.Models.Album;

namespace Cadenza.API.Database.Converters;

internal class AlbumTrackLinkConverter : IAlbumTrackLinkConverter
{
    public AlbumTrackLink ToModel(JsonAlbumTrackLink link)
    {
        return new AlbumTrackLink
        {
            TrackId = link.TrackId,
            AlbumId = link.AlbumId,
            Position = new AlbumTrackPosition(link.DiscNo ?? 1, link.TrackNo)
        };
    }

    public JsonAlbumTrackLink ToJson(AlbumTrackLink link)
    {
        return new JsonAlbumTrackLink
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