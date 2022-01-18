using Cadenza.Domain;

namespace Cadenza.Core;

public interface IMainRepository
{
    Task Clear();
    Task AddArtists(IEnumerable<ArtistInfo> artists);
    Task AddAlbums(IEnumerable<AlbumInfo> albums);
}