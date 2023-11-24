namespace Cadenza.Database.SqlLibrary.Model.Queue;

internal class GetTrackUpdatesResult
{
    public int Id { get; set; }
    public int TrackId { get; set; }
    public string PropertyName { get; set; }
    public string OriginalValue { get; set; }
    public string UpdatedValue { get; set; }
}
