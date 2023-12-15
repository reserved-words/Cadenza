namespace Cadenza.Database.SqlLibrary.Model.Queue;

internal class GetTrackUpdatesResult
{
    public int TrackId { get; set; }
    public string Title { get; set; }
    public string Year { get; set; }
    public string Lyrics { get; set; }
    public int DiscNo { get; set; }
    public int TrackNo { get; set; }
    public int DiscTrackCount { get; set; }
    public string TagList { get; set; }
}
