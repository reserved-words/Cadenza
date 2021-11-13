namespace Cadenza.Common;

public class ArtistFull
{
    public ArtistInfo Artist { get; set; }
    public ICollection<AlbumFull> Albums { get; set; } = new List<AlbumFull>();
}
