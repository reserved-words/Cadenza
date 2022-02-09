namespace Cadenza.Library;

public interface IPlayTrackRepository
{
    Task Populate();
    Task<ListResponse<PlayTrack>> GetAll(int page, int limit);
    Task<ListResponse<PlayTrack>> GetByAlbum(string id, int page, int limit);
    Task<ListResponse<PlayTrack>> GetByArtist(string id, int page, int limit);
}
