using Cadenza.Local.Common.Interfaces.Converters;
using Cadenza.Local.Common.Model.Json;

namespace Cadenza.Local.Services.Converters;

public class JsonToModelConverter : IJsonToModelConverter
{
    private readonly IArtistConverter _artistConverter;
    private readonly IAlbumConverter _albumConverter;
    private readonly ITrackConverter _trackConverter;
    private readonly IAlbumTrackLinkConverter _albumTrackLinkConverter;

    public JsonToModelConverter(IArtistConverter artistConverter, IAlbumConverter albumConverter, ITrackConverter trackConverter, IAlbumTrackLinkConverter albumTrackLinkConverter)
    {
        _artistConverter = artistConverter;
        _albumConverter = albumConverter;
        _trackConverter = trackConverter;
        _albumTrackLinkConverter = albumTrackLinkConverter;
    }

    public AlbumInfo ConvertAlbum(JsonAlbum album, ICollection<JsonArtist> artists)
    {
        return _albumConverter.ToAppModel(album, artists);
    }

    public AlbumTrackLink ConvertAlbumTrackLink(JsonAlbumTrackLink albumTrackLink)
    {
        return _albumTrackLinkConverter.ToAppModel(albumTrackLink);
    }

    public ArtistInfo ConvertArtist(JsonArtist artist)
    {
        return _artistConverter.ToAppModel(artist);
    }

    public TrackInfo ConvertTrack(JsonTrack track, ICollection<JsonArtist> artists)
    {
        return _trackConverter.ToAppModel(track, artists);
    }
}