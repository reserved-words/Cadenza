namespace Cadenza.Source.Spotify.Api.Model;

public class SpotifyApiListResponse<T>
{
    public string href { get; set; }
    public List<T> items { get; set; }
    public int limit { get; set; }
    public string next { get; set; }
    public int offset { get; set; }
    public string previous { get; set; }
    public int total { get; set; }
}
