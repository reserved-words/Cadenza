namespace Cadenza.Source.Spotify.Model;

public class SpotifyPlaylist
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string ArtworkUrl { get; set; }
    public string CreatedBy { get; set; }
    public bool IsInLibrary { get; set; }
}