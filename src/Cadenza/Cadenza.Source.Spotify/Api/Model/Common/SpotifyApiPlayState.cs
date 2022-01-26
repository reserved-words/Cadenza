namespace Cadenza.Source.Spotify;

public class SpotifyApiPlayState
{
    public bool is_playing { get; set; }
    public int? progress_ms { get; set; }
    public SpotifyApiPlayStateItem item { get; set; }

}

public class SpotifyApiPlayStateItem
{
    public int? duration_ms { get; set; }
}