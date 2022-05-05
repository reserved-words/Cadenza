using Cadenza.Local.Common.Interfaces.Converters;
using Cadenza.Local.Common.Model.Json;

namespace Cadenza.Local.Services.Converters;

public class AlbumTrackLinkConverter : IAlbumTrackLinkConverter
{
    public AlbumTrackLink ToAppModel(JsonAlbumTrackLink link)
    {
        return new AlbumTrackLink
        {
            TrackId = link.TrackId,
            AlbumId = link.AlbumId,
            Position = new AlbumTrackPosition(link.DiscNo ?? 1, link.TrackNo)
        };
    }

    public JsonAlbumTrackLink ToJsonModel(AlbumTrackLink link)
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