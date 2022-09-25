using Cadenza.API.Database.Interfaces;
using Cadenza.API.Database.Model;
using Cadenza.Domain.Models.Artist;
using Cadenza.Domain.Models.Album;
using Cadenza.Domain.Models.Track;
using Cadenza.API.Database.Interfaces.Converters;

namespace Cadenza.API.Database.Services;

internal class JsonToModelConverter : IJsonToModelConverter
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
        return _albumConverter.ToModel(album, artists);
    }

    public AlbumTrackLink ConvertAlbumTrackLink(JsonAlbumTrackLink albumTrackLink)
    {
        return _albumTrackLinkConverter.ToModel(albumTrackLink);
    }

    public ArtistInfo ConvertArtist(JsonArtist artist)
    {
        return _artistConverter.ToModel(artist);
    }

    public TrackInfo ConvertTrack(JsonTrack track, ICollection<JsonArtist> artists)
    {
        return _trackConverter.ToModel(track, artists);
    }
}