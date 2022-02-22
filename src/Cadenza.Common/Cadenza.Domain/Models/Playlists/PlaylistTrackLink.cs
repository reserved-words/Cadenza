namespace Cadenza.Domain;
public class PlaylistTrackLink
{
    public string PlaylistId { get; set; }
    public string TrackId { get; set; }
    public int Position { get; set; } = new();
}
