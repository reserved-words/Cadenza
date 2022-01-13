namespace Cadenza.Local;

public interface IAlbumConverter
{
    JsonAlbum ToJsonModel(AlbumInfo artist);
    AlbumInfo ToAppModel(JsonAlbum artist, ICollection<JsonArtist> artists);
    JsonAlbum ToJsonModel(Id3Data id3Data);
}