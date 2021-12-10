namespace Cadenza.Library;

public interface ICombinedSourceLibrary
{
    Task<ICollection<Artist>> GetAlbumArtists(IEnumerable<LibrarySource> enabledSources);
    Task<ICollection<Track>> GetAllTracks(IEnumerable<LibrarySource> enabledSources);
    Task<ArtistFull> GetAlbumArtist(string id, IEnumerable<LibrarySource> enabledSources);
    Task<TrackFull> GetTrack(string id, LibrarySource source);
    //Task<TrackSummary> GetTrackSummary(string id, LibrarySource source);
}
