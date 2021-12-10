namespace Cadenza.Common;

public interface ISourceRepository
{
    Task<ICollection<ArtistInfo>> GetArtists();
    Task<ICollection<AlbumInfo>> GetAlbums();
    Task<TrackInfo> GetTrack(string id);
    Task<List<string>> GetAlbumTracks(string artistId, string albumId);
    Task<List<string>> GetArtistTracks(string id);
}
