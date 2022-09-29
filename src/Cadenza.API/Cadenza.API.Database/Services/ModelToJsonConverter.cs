using Cadenza.Common.Domain.Model.Album;
using Cadenza.Common.Domain.Model.Artist;
using Cadenza.Common.Domain.Model.Track;

namespace Cadenza.API.Database.Services;

internal class ModelToJsonConverter : IModelToJsonConverter
{
    private readonly IArtistConverter _artistConverter;
    private readonly IAlbumConverter _albumConverter;
    private readonly ITrackConverter _trackConverter;
    private readonly IAlbumTrackConverter _albumTrackLinkConverter;

    public ModelToJsonConverter(IArtistConverter artistConverter, IAlbumConverter albumConverter, ITrackConverter trackConverter, IAlbumTrackConverter albumTrackLinkConverter)
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

    public JsonAlbumTrack ConvertAlbumTrackLink(AlbumTrackLink albumTrackLink)
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