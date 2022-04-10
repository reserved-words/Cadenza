using Cadenza.Local.Common.Interfaces.Converters;
using Cadenza.Local.Common.Model.Json;

namespace Cadenza.Local.Services.Converters;

public class AlbumTrackLinkConverter : IAlbumTrackLinkConverter
{
    private readonly IBase64Converter _base64Converter;

    public AlbumTrackLinkConverter(IBase64Converter base64Converter)
    {
        _base64Converter = base64Converter;
    }

    public AlbumTrackLink ToAppModel(JsonAlbumTrackLink link)
    {
        return new AlbumTrackLink
        {
            TrackId = _base64Converter.ToBase64(link.TrackPath),
            AlbumId = link.AlbumId,
            Position = new AlbumTrackPosition(link.DiscNo ?? 1, link.TrackNo)
        };
    }

    public JsonAlbumTrackLink ToJsonModel(AlbumTrackLink link)
    {
        return new JsonAlbumTrackLink
        {
            TrackPath = _base64Converter.FromBase64(link.TrackId),
            AlbumId = link.AlbumId,
            DiscNo = link.Position.DiscNo == 1
                    ? null
                    : link.Position.DiscNo,
            TrackNo = link.Position.TrackNo
        };
    }
}