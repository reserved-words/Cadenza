namespace Cadenza.Local.API;

public interface ILibraryService
{
    Task<ArtistFull> GetAlbumArtist(string artistId);
    Task<ICollection<Artist>> GetAlbumArtists();
    Task<TrackFull> GetTrack(string id);
    Task<TrackSummary> GetTrackSummary(string id);
}
