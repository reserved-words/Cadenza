namespace Cadenza.Database.SqlLibrary.Model.Update;

internal class GetTrackForUpdateResult
{
    public int DiscNo { get; set; }
    public int TrackNo { get; set; }
    public string Title { get; set; }
    public string Year { get; set; }
    public string Lyrics { get; set; }
    public string TagList { get; set; }
}
