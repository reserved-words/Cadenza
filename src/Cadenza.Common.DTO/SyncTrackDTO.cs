namespace Cadenza.Common.DTO;

public class SyncTrack
{
    public string IdFromSource { get; set; }
    public string Title { get; set; }
    public int DurationSeconds { get; set; }
    public string Year { get; set; }
    public string Lyrics { get; set; }
    public string TagList { get; set; }
    public int DiscNo { get; set; }
    public int TrackNo { get; set; }

    public SyncArtist Artist { get; set; }
    public SyncAlbum Album { get; set; }
}
