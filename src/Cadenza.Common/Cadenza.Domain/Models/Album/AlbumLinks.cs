namespace Cadenza.Domain;

public class AlbumLinks
{
    public string AlbumId { get; set; }
    public List<AlbumTrackLink> Tracks { get; set; } = new List<AlbumTrackLink>();
    public string ArtistId { get; set; }
}
