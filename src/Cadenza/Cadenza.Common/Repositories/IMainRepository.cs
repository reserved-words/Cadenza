using Cadenza.Domain;

namespace Cadenza.Common;

public interface IMainRepository
{
    Task Clear();
    Task AddArtists(ICollection<ArtistInfo> artists);
    Task AddAlbums(ICollection<AlbumInfo> albums);
}