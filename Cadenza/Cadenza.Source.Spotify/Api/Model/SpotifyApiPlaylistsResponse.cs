namespace Cadenza.Source.Spotify;

internal class SpotifyApiPlaylistsResponse
{
    public string href { get; set; }
    public List<SpotifyApiPlaylist> items { get; set; }
    public int limit { get; set; }
    public string next { get; set; }
    public int offset { get; set; }
    public string previous { get; set; }
    public int total { get; set; }

}