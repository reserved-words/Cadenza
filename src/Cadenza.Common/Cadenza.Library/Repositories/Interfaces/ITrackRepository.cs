namespace Cadenza.Library;

public interface ITrackRepository
{
    Task<TrackFull> GetTrack(string id);
}
