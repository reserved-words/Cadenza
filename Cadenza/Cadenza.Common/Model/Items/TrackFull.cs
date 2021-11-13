namespace Cadenza.Common;

public class TrackFull
{
    public TrackInfo Track { get; set; }
    public ArtistInfo Artist { get; set; }
    public AlbumInfo Album { get; set; }
    public AlbumTrackPosition Position { get; set; } = new();

    public string DiscPosition => $"Disc {Position.DiscNo} of {Album.DiscCount}";
    public string TrackPosition => $"Track {Position.TrackNo} of {Album.TrackCounts[TrackCountIndex]}";
    public string FormattedDuration => TimeSpan.FromSeconds(Track.DurationSeconds).ToString(@"hh\:mm\:ss");
    public string ReleaseType => Album.ReleaseType.GetDisplayName();

    private int TrackCountIndex => Math.Max(0, Position.DiscNo - 1);
}
