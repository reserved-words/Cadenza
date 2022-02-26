namespace Cadenza.Library;

public interface IPlayTrackRepository
{
    Task<ListResponse<PlayTrack>> GetAll(int page, int limit);
    Task<ListResponse<PlayTrack>> GetByAlbum(string id, int page, int limit);
    Task<ListResponse<PlayTrack>> GetByArtist(string id, int page, int limit);
    Task<ListResponse<PlayTrack>> GetByGenre(string id, int page, int limit);
    Task<ListResponse<PlayTrack>> GetByGrouping(Grouping id, int page, int limit);
}
