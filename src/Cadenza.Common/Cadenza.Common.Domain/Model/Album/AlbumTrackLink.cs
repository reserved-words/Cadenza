namespace Cadenza.Common.Domain.Model.Album;

public class AlbumTrackLink
{
    public string AlbumId { get; set; }
    public string TrackId { get; set; }
    public AlbumTrackPosition Position { get; set; } = new();
}
