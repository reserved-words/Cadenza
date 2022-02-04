namespace Cadenza.Domain;

public interface ISearchRepository
{
    public LibrarySource Source { get; }
    Task<ListResponse<SearchableAlbum>> GetSearchAlbums(int page, int limit);
    Task<ListResponse<SearchableArtist>> GetSearchArtists(int page, int limit);
    Task<ListResponse<SearchablePlaylist>> GetSearchPlaylists(int page, int limit);
    Task<ListResponse<SearchableTrack>> GetSearchTracks(int page, int limit);
}