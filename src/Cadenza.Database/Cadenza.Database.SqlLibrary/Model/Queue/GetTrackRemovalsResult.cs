namespace Cadenza.Database.SqlLibrary.Model.Queue;

internal class GetTrackRemovalsResult
{
    public int RequestId { get; set; }
    public int SourceId { get; set; }
    public string TrackIdFromSource { get; set; }
}