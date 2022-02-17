namespace Cadenza.Source.Spotify.Player;

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

public class SpotifyApiDevicesResponse
{
    public SpotifyApiDevice[] Devices { get; set; }
}

public class SpotifyApiDevice
{
    public string id { get; set; }
    public bool is_active { get; set; }
    public bool is_private_session { get; set; }
    public bool is_restricted { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public int? volume_percent { get; set; }
}