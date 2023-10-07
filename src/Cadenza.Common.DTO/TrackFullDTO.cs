namespace Cadenza.Common.DTO;

public class TrackFullDTO
{
    public TrackDetailsDTO Track { get; set; } = new();
    public ArtistDetailsDTO Artist { get; set; } = new();
    public AlbumDetailsDTO Album { get; set; } = new();
    public ArtistDetailsDTO AlbumArtist { get; set; } = new();
    public AlbumTrackLinkDTO AlbumTrack { get; set; } = new();

    public int Id => Track.Id;
    public LibrarySource Source => Track.Source;
    public string IdFromSource => Track.IdFromSource;
    public int Duration => Track.DurationSeconds;
}
