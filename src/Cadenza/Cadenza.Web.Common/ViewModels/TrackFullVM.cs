namespace Cadenza.Web.Common.ViewModels;

public class TrackFullVM
{
    public TrackDetailsVM Track { get; set; } = new();
    public ArtistDetailsVM Artist { get; set; } = new();
    public AlbumDetailsVM Album { get; set; } = new();
    public ArtistDetailsVM AlbumArtist { get; set; } = new();
    public AlbumTrackLinkVM AlbumTrack { get; set; } = new();

    public int Id => Track.Id;
    public LibrarySource Source => Track.Source;
    public string IdFromSource => Track.IdFromSource;
    public int Duration => Track.DurationSeconds;
}
