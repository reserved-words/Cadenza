namespace Cadenza.Common.Domain.Model.Library;

public class FullLibrary
{
    public List<ArtistDetails> Artists { get; set; } = new();

    public List<TrackDetails> Tracks { get; set; } = new();

    public List<AlbumDetails> Albums { get; set; } = new();

    public List<AlbumTrackLink> AlbumTracks { get; set; } = new();
}
