using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Web.Common.Interfaces.Library;

public interface IArtistRepository
{
    Task<List<Artist>> GetAllArtists();
    Task<ArtistDetails> GetArtist(int id);
    Task<List<Album>> GetAlbums(int artistId);
    Task<List<Album>> GetAlbumsFeaturingArtist(int artistId);
    Task<List<Artist>> GetArtistsByGenre(string id);
    Task<List<Artist>> GetArtistsByGrouping(int id);
    Task<List<Track>> GetArtistTracks(int id);
}
