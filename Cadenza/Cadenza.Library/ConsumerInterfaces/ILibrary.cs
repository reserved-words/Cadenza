namespace Cadenza.Library;

public interface ILibrary
{
    Task<ICollection<Artist>> GetAlbumArtists();
    Task<ICollection<Track>> GetAllTracks();
    Task<ArtistFull> GetAlbumArtist(string id);
    Task<TrackFull> GetTrack(string id);
    Task<TrackSummary> GetTrackSummary(string id);
}
