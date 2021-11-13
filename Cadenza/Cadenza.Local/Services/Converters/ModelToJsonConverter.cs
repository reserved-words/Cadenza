namespace Cadenza.Local;

public class ModelToJsonConverter : IModelToJsonConverter
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
        return _albumConverter.ToJsonModel(album);
    }

    public JsonAlbumTrackLink ConvertAlbumTrackLink(AlbumTrackLink albumTrackLink)
    {
        return _albumTrackLinkConverter.ToJsonModel(albumTrackLink);
    }

    public JsonArtist ConvertArtist(ArtistInfo artist)
    {
        return _artistConverter.ToJsonModel(artist);
    }

    public JsonTrack ConvertTrack(TrackInfo track)
    {
        return _trackConverter.ToJsonModel(track);
    }
}
