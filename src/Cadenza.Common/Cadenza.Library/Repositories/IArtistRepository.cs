using Cadenza.Domain.Enums;
using Cadenza.Domain.Model.Album;
using Cadenza.Domain.Model.Artist;
using Cadenza.Domain.Model.Update;

namespace Cadenza.Library.Repositories;

public interface IArtistRepository
{
    Task<List<Artist>> GetAlbumArtists();
    Task<List<Artist>> GetAllArtists();
    Task<List<Artist>> GetTrackArtists();
    Task<ArtistInfo> GetArtist(string id);
    Task<List<Album>> GetAlbums(string artistId);
    Task<List<Artist>> GetArtistsByGenre(string id);
    Task<List<Artist>> GetArtistsByGrouping(Grouping id);
}
