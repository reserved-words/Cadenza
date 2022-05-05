namespace Cadenza.Library;

public interface IArtistRepository
{
    Task<List<Artist>> GetAlbumArtists();
    Task<List<Artist>> GetAllArtists();
    Task<List<Artist>> GetTrackArtists();
    Task<ArtistInfo> GetArtist(string id);
    Task<List<Album>> GetAlbums(string artistId);
    Task<List<Artist>> GetArtistsByGenre(string id);
    Task<List<Artist>> GetArtistsByGrouping(Grouping id);
    Task UpdateArtist(ArtistUpdate update);
}
