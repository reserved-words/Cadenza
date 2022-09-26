using Cadenza.API.Database.Interfaces;
using Cadenza.API.Database.Model;
using Cadenza.Domain.Models.Artist;
using Cadenza.Domain.Models.Album;
using Cadenza.Domain.Models.Track;
using Cadenza.API.Database.Interfaces.Converters;

namespace Cadenza.API.Database.Services;

internal class ModelToJsonConverter : IModelToJsonConverter
{
    private readonly IArtistConverter _artistConverter;
    private readonly IAlbumConverter _albumConverter;
    private readonly ITrackConverter _trackConverter;
    private readonly IAlbumTrackLinkConverter _albumTrackLinkConverter;

    public ModelToJsonConverter(IArtistConverter artistConverter, IAlbumConverter albumConverter, ITrackConverter trackConverter, IAlbumTrackLinkConverter albumTrackLinkConverter)
    {
        _artistConverter = artistConverter;
        _albumConverter = albumConverter;
        _trackConverter = trackConverter;
        _albumTrackLinkConverter = albumTrackLinkConverter;
    }

    public JsonAlbum ConvertAlbum(AlbumInfo album)
    {
        return _albumConverter.ToJson(album);
    }

    public JsonAlbumTrackLink ConvertAlbumTrackLink(AlbumTrackLink albumTrackLink)
    {
        return _albumTrackLinkConverter.ToJson(albumTrackLink);
    }

    public JsonArtist ConvertArtist(ArtistInfo artist)
    {
        return _artistConverter.ToJson(artist);
    }

    public JsonTrack ConvertTrack(TrackInfo track)
    {
        return _trackConverter.ToJson(track);
    }
}