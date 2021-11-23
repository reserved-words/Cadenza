namespace Cadenza.Database;

public interface ITrackRepository
{
    Task<TrackSummary> GetSummary(string id);
    Task<TrackDetail> GetDetail(string id);
}
