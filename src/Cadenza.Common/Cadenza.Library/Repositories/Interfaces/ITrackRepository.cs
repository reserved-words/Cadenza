namespace Cadenza.Library;

public interface ITrackRepository
{
    Task<TrackFull> GetTrack(string id);
    Task UpdateTrack(TrackUpdate update);
}
