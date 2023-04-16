namespace Cadenza.API.Interfaces.Controllers;

public interface IPlayTrackService
{
    Task<List<PlayTrack>> GetPlayTracks();
    Task<List<PlayTrack>> GetPlayTracksByAlbum(int id);
    Task<List<PlayTrack>> GetPlayTracksByArtist(int id);
    Task<List<PlayTrack>> GetPlayTracksByGenre(string id);
    Task<List<PlayTrack>> GetPlayTracksByGrouping(Grouping id);
    Task<List<PlayTrack>> GetPlayTracksByTag(string id);
}
