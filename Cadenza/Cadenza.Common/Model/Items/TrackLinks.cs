namespace Cadenza.Common;

public class TrackLinks
{
    public string TrackId { get; set; }
    public string ArtistId { get; set; }
    public string AlbumId { get; set; }
    public AlbumTrackPosition Position { get; set; } = new();
}
