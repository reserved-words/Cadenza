namespace Cadenza.Domain;

public class TrackFull
{
    public TrackInfo Track { get; set; } = new();
    public ArtistInfo Artist { get; set; } = new();
    public AlbumInfo Album { get; set; } = new();
    public ArtistInfo AlbumArtist { get; set; } = new();
    public AlbumTrackLink AlbumTrack { get; set; } = new();
}
