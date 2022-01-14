namespace Cadenza.Domain;

public class FullTrack : PlayingTrack
{
    public string ArtistId { get; set; }
    public string AlbumId { get; set; }
    public string AlbumArtistId { get; set; }

    public string Lyrics { get; set; }
    public List<string> Tags { get; set; } = new List<string>();

    public int DiscNo { get; set; }
    public int DiscCount { get; set; }
    public int TrackNo { get; set; }
    public int TrackCount { get; set; }

    public string DiscPosition => $"Disc {DiscNo} of {DiscCount}";
    public string TrackPosition => $"Track {TrackNo} of {TrackCount}";
    public string FormattedDuration => TimeSpan.FromSeconds(DurationSeconds).ToString(@"hh\:mm\:ss");

    public string AlbumYear { get; set; }
}
