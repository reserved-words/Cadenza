namespace Cadenza.Domain;

public class ArtistFull
{
    public ArtistInfo Artist { get; set; }
    public ICollection<AlbumFull> Albums { get; set; } = new List<AlbumFull>();
}
