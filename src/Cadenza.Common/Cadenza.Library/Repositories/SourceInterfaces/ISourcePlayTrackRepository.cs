namespace Cadenza.Library;

public interface ISourcePlayTrackRepository
{
    public LibrarySource Source { get; }
    Task<ListResponse<PlayTrack>> GetAll(int page, int limit);
    Task<ListResponse<PlayTrack>> GetByAlbum(string id, int page, int limit);
    Task<ListResponse<PlayTrack>> GetByArtist(string id, int page, int limit);
}
