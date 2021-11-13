namespace Cadenza.Local;


public class AlbumTrackLinkConverter : IAlbumTrackLinkConverter
{
    private readonly IBase64Converter _base64Converter;
    private readonly IIdGenerator _idGenerator;

    public AlbumTrackLinkConverter(IBase64Converter base64Converter, IIdGenerator idGenerator)
    {
        _base64Converter = base64Converter;
        _idGenerator = idGenerator;
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

    public JsonAlbumTrackLink ToJsonModel(Id3Data id3Data)
    {
        // need a single place to do these conversions

        var albumId = _idGenerator.GenerateAlbumId(id3Data.Album.ArtistName, id3Data.Album.Title);

        return new JsonAlbumTrackLink
        {
            TrackPath = id3Data.Track.Filepath,
            AlbumId = albumId,
            DiscNo = id3Data.Disc.DiscNo,
            TrackNo = id3Data.Track.TrackNo
        };
    }
}