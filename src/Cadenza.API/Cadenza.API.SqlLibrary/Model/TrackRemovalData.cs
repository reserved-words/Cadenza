namespace Cadenza.API.SqlLibrary.Model;

internal class TrackRemovalData : NewTrackRemovalData
{
    public int Id { get; set; }
    public int SourceId { get; set; }
}