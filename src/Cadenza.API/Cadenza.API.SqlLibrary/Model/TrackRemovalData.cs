namespace Cadenza.API.SqlLibrary.Model;

internal class TrackRemovalData : NewTrackRemovalData
{
    public int RequestId { get; set; }
    public int SourceId { get; set; }
}