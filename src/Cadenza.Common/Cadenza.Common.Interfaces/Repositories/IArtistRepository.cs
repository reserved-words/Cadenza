using Cadenza.Common.Domain.Enums;
using Cadenza.Common.Domain.Model.Album;
using Cadenza.Common.Domain.Model.Artist;
using Cadenza.Common.Domain.Model.Track;

namespace Cadenza.Common.Interfaces.Repositories;

public interface IArtistRepository
{
    Task<List<Artist>> GetAllArtists();
    Task<ArtistInfo> GetArtist(int id);
    Task<List<Album>> GetAlbums(int artistId);
    Task<List<Artist>> GetArtistsByGenre(string id);
    Task<List<Artist>> GetArtistsByGrouping(Grouping id);
    Task<List<Track>> GetArtistTracks(int id);
}
