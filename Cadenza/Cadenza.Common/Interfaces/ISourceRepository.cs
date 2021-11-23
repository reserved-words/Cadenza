namespace Cadenza.Common;

public interface ISourceRepository
{
    Task<ICollection<ArtistInfo>> GetArtists();
    Task<ICollection<AlbumInfo>> GetAlbums();
    Task<ICollection<TrackInfo>> GetTracks();
    Task<ICollection<AlbumTrackLink>> GetAlbumTrackLinks();
}
