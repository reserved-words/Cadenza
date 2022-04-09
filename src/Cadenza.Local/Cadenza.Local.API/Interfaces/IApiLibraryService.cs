namespace Cadenza.Local.API.Interfaces;

public interface IApiLibraryService
{
    Task Populate();
    Task<ListResponse<Artist>> GetAllArtists(int page, int limit);
    Task<ListResponse<Artist>> GetAlbumArtists(int page, int limit);
    Task<ListResponse<Artist>> GetTrackArtists(int page, int limit);
    Task<ListResponse<Artist>> GetArtistsByGrouping(Grouping id, int page, int limit);
    Task<ListResponse<Artist>> GetArtistsByGenre(string id, int page, int limit);
    Task<ArtistInfo> GetArtist(string id);
    Task<ListResponse<Album>> GetAlbums(string id, int page, int limit);
    Task<TrackFull> GetTrack(string id);
    Task<AlbumInfo> GetAlbum(string id);
    Task<List<AlbumTrack>> GetTracks(string albumId);
}
