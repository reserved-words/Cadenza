namespace Cadenza.Common.DTO;

public class SyncTrackDTO
{
    public string IdFromSource { get; set; }
    public string Title { get; set; }
    public int DurationSeconds { get; set; }
    public string Year { get; set; }
    public string Lyrics { get; set; }
    public string TagList { get; set; }
    public int DiscNo { get; set; }
    public int DiscTrackCount { get; set; }
    public int TrackNo { get; set; }

    public SyncArtistDTO Artist { get; set; }
    public SyncAlbumDTO Album { get; set; }
}
