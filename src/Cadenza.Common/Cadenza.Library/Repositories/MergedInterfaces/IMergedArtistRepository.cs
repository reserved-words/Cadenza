namespace Cadenza.Library;

public interface IMergedArtistRepository
{
    Task<List<Artist>> GetAlbumArtists();
    Task<List<Artist>> GetAllArtists();
    Task<List<Artist>> GetTrackArtists();
    Task<ArtistInfo> GetArtist(string id);
    Task<List<Album>> GetArtistAlbums(string artistId);
}
