namespace Cadenza.Library;

public interface IMergedArtistRepository
{
    Task<List<Artist>> GetAlbumArtists();
    Task<List<Artist>> GetAllArtists();
    Task<List<Artist>> GetTrackArtists();
    Task<List<Artist>> GetArtistsByGrouping(Grouping id);
    Task<List<Artist>> GetArtistsByGenre(string id);
    Task<ArtistInfo> GetArtist(string id);
    Task<List<Album>> GetArtistAlbums(string artistId);
    Task UpdateArtist(ArtistUpdate update);
}
