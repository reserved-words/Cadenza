using Cadenza.Domain.Model;
using Cadenza.Domain.Model.Album;
using Cadenza.Domain.Model.Artist;
using Cadenza.Domain.Model.Track;

namespace Cadenza.API.Database.Services;

internal class JsonToModelConverter : IJsonToModelConverter
{
    private readonly IArtistConverter _artistConverter;
    private readonly IAlbumConverter _albumConverter;
    private readonly ITrackConverter _trackConverter;
    private readonly IAlbumTrackConverter _albumTrackLinkConverter;

    public JsonToModelConverter(IArtistConverter artistConverter, IAlbumConverter albumConverter, ITrackConverter trackConverter, IAlbumTrackConverter albumTrackLinkConverter)
    {
        _artistConverter = artistConverter;
        _albumConverter = albumConverter;
        _trackConverter = trackConverter;
        _albumTrackLinkConverter = albumTrackLinkConverter;
    }

    public FullLibrary Convert(JsonItems items)
    {
        return new FullLibrary
        {
            Artists = items.Artists.Select(a => ConvertArtist(a)).ToList(),
            Albums = items.Albums.Select(a => ConvertAlbum(a, items.Artists)).ToList(),
            Tracks = items.Tracks.Select(a => ConvertTrack(a, items.Artists)).ToList(),
            AlbumTracks = items.AlbumTracks.Select(a => ConvertAlbumTrack(a)).ToList(),
        };
    }

    public AlbumInfo ConvertAlbum(JsonAlbum album, ICollection<JsonArtist> artists)
    {
        return _albumConverter.ToModel(album, artists);
    }

    public AlbumTrackLink ConvertAlbumTrack(JsonAlbumTrack albumTrackLink)
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