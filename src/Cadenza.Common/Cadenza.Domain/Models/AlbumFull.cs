namespace Cadenza.Domain;

public class AlbumFull
{
    public AlbumInfo Album { get; set; }
    public ICollection<AlbumTrack> AlbumTracks { get; set; } = new List<AlbumTrack>();
}
