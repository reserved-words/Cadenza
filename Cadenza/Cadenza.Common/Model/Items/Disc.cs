namespace Cadenza.Common;

public class Disc
{
    public int DiscNo { get; set; }
    public int TrackCount { get; set; }
    public ICollection<AlbumTrack> Tracks { get; set; } = new List<AlbumTrack>();

    public string Name => $"Disc {DiscNo}";
}
