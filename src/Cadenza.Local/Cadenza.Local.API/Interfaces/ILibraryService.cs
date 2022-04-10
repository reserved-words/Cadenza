namespace Cadenza.Local.API.Interfaces;

public interface ILibraryService
{
    Task Populate();
    Task<List<Artist>> GetAllArtists();
    Task<List<Artist>> GetAlbumArtists();
    Task<List<Artist>> GetTrackArtists();
    Task<List<Artist>> GetArtistsByGrouping(Grouping id);
    Task<List<Artist>> GetArtistsByGenre(string id);
    Task<ArtistInfo> GetArtist(string id);
    Task<List<Album>> GetAlbums(string id);
    Task<TrackFull> GetTrack(string id);
    Task<AlbumInfo> GetAlbum(string id);
    Task<List<AlbumTrack>> GetTracks(string albumId);
}
