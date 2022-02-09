namespace Cadenza.Library;

public interface ITrackRepository
{
    Task Populate();
    Task<TrackFull> GetTrack(string id);
}
