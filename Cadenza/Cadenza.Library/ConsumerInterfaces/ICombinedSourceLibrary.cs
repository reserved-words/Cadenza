namespace Cadenza.Library;

public interface ICombinedSourceLibrary
{
    Task<ICollection<Artist>> GetAlbumArtists(IEnumerable<Source> enabledSources);
    Task<ICollection<Track>> GetAllTracks(IEnumerable<Source> enabledSources);
    Task<ArtistFull> GetAlbumArtist(string id, IEnumerable<Source> enabledSources);
    Task<TrackFull> GetTrack(string id, Source source);
    Task<TrackSummary> GetTrackSummary(string id, Source source);
}
