namespace Cadenza.Source.Spotify;

public class SpotifyApiPlayState
{
    public int timestamp { get; set; }
    public int? progress_ms { get; set; }
    public SpotifyApiPlayStateItem item { get; set; }

}

public class SpotifyApiPlayStateItem
{
    public int? duration_ms { get; set; }
}