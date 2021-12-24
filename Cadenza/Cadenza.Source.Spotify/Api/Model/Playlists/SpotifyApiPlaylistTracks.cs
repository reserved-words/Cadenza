namespace Cadenza.Source.Spotify;

public class SpotifyApiPlaylistTracks
{
    public string href { get; set; }
    public object[] items { get; set; }
    public int limit { get; set; }
    public string next { get; set; }
    public int offset { get; set; }
    public string previous { get; set; }
    public int total { get; set; }
}