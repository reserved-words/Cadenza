using Cadenza.Common;

namespace Cadenza.Database;

public interface IMainRepository
{
    Task Clear();
    Task AddArtists(ICollection<ArtistInfo> artists);
    Task AddAlbums(ICollection<AlbumInfo> albums);
    Task AddTracks(ICollection<TrackInfo> tracks);
    Task AddAlbumTrackLinks(ICollection<AlbumTrackLink> tracks);
}