namespace Cadenza.Library;

public interface IMergedPlayTrackRepository
{
    Task<List<PlayTrack>> GetAll();
    Task<List<PlayTrack>> GetByAlbum(string id);
    Task<List<PlayTrack>> GetByArtist(string id);
    Task<List<PlayTrack>> GetByGenre(string id);
    Task<List<PlayTrack>> GetByGrouping(Grouping id);
}
