namespace Cadenza.Common.Domain.Model.Library;

public class TrackFull
{
    public TrackDetails Track { get; set; } = new();
    public ArtistDetails Artist { get; set; } = new();
    public AlbumDetails Album { get; set; } = new();
    public ArtistDetails AlbumArtist { get; set; } = new();
    public AlbumTrackLink AlbumTrack { get; set; } = new();

    public int Id => Track.Id;
    public LibrarySource Source => Track.Source;
    public string IdFromSource => Track.IdFromSource;
    public int Duration => Track.DurationSeconds;
}
