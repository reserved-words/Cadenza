namespace Cadenza.Common.Domain.Model.Library;

public class TrackFull
{
    public TrackInfo Track { get; set; } = new();
    public ArtistInfo Artist { get; set; } = new();
    public AlbumInfo Album { get; set; } = new();
    public ArtistInfo AlbumArtist { get; set; } = new();
    public AlbumTrackLink AlbumTrack { get; set; } = new();

    public int Id => Track.Id;
    public LibrarySource Source => Track.Source;
    public string IdFromSource => Track.IdFromSource;
    public int Duration => Track.DurationSeconds;
}
