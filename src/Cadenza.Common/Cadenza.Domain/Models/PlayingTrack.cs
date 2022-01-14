namespace Cadenza.Domain;

public class PlayingTrack
{
    public string Id { get; set; }
    public LibrarySource Source { get; set; }
    public int DurationSeconds { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string AlbumTitle { get; set; }
    public string AlbumArtist { get; set; }
    public string ArtworkUrl { get; set; }
    public ReleaseType ReleaseType { get; set; }
    public string Year { get; set; }
}
