namespace Cadenza.Local;

public interface IAlbumTrackLinkConverter
{
    JsonAlbumTrackLink ToJsonModel(AlbumTrackLink link);
    AlbumTrackLink ToAppModel(JsonAlbumTrackLink link);
    JsonAlbumTrackLink ToJsonModel(Id3Data id3Data);
}
