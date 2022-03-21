namespace Cadenza.Library;

public interface IPlayTrackRepository
{
    Task<List<PlayTrack>> GetByAlbum(string id);

    Task<ListResponse<PlayTrack>> GetAll(int page, int limit);
    Task<ListResponse<PlayTrack>> GetByArtist(string id, int page, int limit);
    Task<ListResponse<PlayTrack>> GetByGenre(string id, int page, int limit);
    Task<ListResponse<PlayTrack>> GetByGrouping(Grouping id, int page, int limit);
}
