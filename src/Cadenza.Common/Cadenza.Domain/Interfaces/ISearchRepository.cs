namespace Cadenza.Domain;

public interface ISearchRepository
{
    Task<ListReponse<SearchableAlbum>> GetSearchAlbums(int page, int limit);
    Task<ListReponse<SearchableArtist>> GetSearchArtists(int page, int limit);
    Task<ListReponse<SearchablePlaylist>> GetSearchPlaylists(int page, int limit);
    Task<ListReponse<SearchableTrack>> GetSearchTracks(int page, int limit);
}