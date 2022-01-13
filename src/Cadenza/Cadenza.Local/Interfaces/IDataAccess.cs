namespace Cadenza.Local;

public interface IDataAccess
{
    List<JsonArtist> GetArtists();
    List<JsonAlbum> GetAlbums();
    List<JsonTrack> GetTracks();
    List<JsonAlbumTrackLink> GetAlbumTrackLinks();
    JsonItems GetAll();
    JsonUpdateHistory GetUpdateHistory();

    void SaveArtists(List<JsonArtist> artists);
    void SaveAlbums(List<JsonAlbum> albums);
    void SaveTracks(List<JsonTrack> tracks);
    void SaveAlbumTrackLinks(List<JsonAlbumTrackLink> albumTrackLinks);
    void SaveAll(JsonItems items);
    void SaveUpdateHistory(JsonUpdateHistory history);
}