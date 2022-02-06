namespace Cadenza.Local;

public interface IDataAccess
{
    Task<List<JsonArtist>> GetArtists();
    Task<List<JsonAlbum>> GetAlbums();
    Task<List<JsonTrack>> GetTracks();
    Task<List<JsonAlbumTrackLink>> GetAlbumTrackLinks();
    Task<JsonItems> GetAll();
    Task<JsonUpdateHistory> GetUpdateHistory();

    Task SaveArtists(List<JsonArtist> artists);
    Task SaveAlbums(List<JsonAlbum> albums);
    Task SaveTracks(List<JsonTrack> tracks);
    Task SaveAlbumTrackLinks(List<JsonAlbumTrackLink> albumTrackLinks);
    Task SaveAll(JsonItems items);
    Task SaveUpdateHistory(JsonUpdateHistory history);
}