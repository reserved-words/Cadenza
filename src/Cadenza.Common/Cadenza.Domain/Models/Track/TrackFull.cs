namespace Cadenza.Domain;

public class TrackFull
{
    public TrackInfo Track { get; set; }
    public ArtistInfo Artist { get; set; }
    public AlbumInfo Album { get; set; }
    public ArtistInfo AlbumArtist { get; set; }
    public AlbumTrackLink AlbumTrack { get; set; }
}
