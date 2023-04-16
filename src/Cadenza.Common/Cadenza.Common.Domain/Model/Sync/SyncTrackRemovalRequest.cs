namespace Cadenza.Common.Domain.Model.Sync;

public class SyncTrackRemovalRequest
{
    public int RequestId { get; set; }
    public string TrackIdFromSource { get; set; }
}
