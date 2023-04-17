namespace Cadenza.API.SqlLibrary.Model;

internal class TrackRemovalData
{
    public int RequestId { get; set; }
    public int SourceId { get; set; }
    public string TrackIdFromSource { get; set; }
}