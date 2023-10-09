namespace Cadenza.Web.Model;

public record TrackFullVM
{
    public TrackDetailsVM Track { get; init; }
    public ArtistDetailsVM Artist { get; init; }
    public AlbumDetailsVM Album { get; init; }
    public ArtistDetailsVM AlbumArtist { get; init; }
    public AlbumTrackLinkVM AlbumTrack { get; init; }

    public int Id => Track.Id;
    public LibrarySource Source => Track.Source;
    public string IdFromSource => Track.IdFromSource;
    public int Duration => Track.DurationSeconds;
}
