namespace Cadenza.Source.Spotify.Model;

public class SpotifyAlbum
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Year { get; set; }
    public string ArtworkUrl { get; set; }
    public bool IsInLibrary { get; set; }
}
