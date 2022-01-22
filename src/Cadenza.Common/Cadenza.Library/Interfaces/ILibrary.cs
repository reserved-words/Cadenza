namespace Cadenza.Library;

public interface ILibrary
{
    Task<IEnumerable<ArtistInfo>> GetArtists();
    Task<IEnumerable<AlbumInfo>> GetAlbums();
    Task<TrackSummary> GetTrack(string id);
    Task<IEnumerable<BasicTrack>> GetAllTracks();
    Task<TrackFull> GetFullTrack(string id);
}
