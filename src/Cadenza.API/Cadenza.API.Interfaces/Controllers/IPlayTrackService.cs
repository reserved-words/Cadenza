namespace Cadenza.API.Interfaces.Controllers;

public interface IPlayTrackService
{
    Task<List<int>> GetPlayTracks();
    Task<List<int>> GetPlayTracksByAlbum(int id);
    Task<List<int>> GetPlayTracksByArtist(int id);
    Task<List<int>> GetPlayTracksByGenre(string id);
    Task<List<int>> GetPlayTracksByGrouping(int id);
    Task<List<int>> GetPlayTracksByTag(string id);
}
