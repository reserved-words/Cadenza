namespace Cadenza.Local.Common.Model.Id3;

public class TrackId3Data
{
    public string Filepath { get; set; }
    public string Title { get; set; }
    public int TrackNo { get; set; }
    public TimeSpan Duration { get; set; }
    public string Lyrics { get; set; }
    public string Comment { get; set; }
}