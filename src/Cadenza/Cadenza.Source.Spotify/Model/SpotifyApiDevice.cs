namespace Cadenza.Source.Spotify.Model;

internal class SpotifyApiDevice
{
    public string id { get; set; }
    public bool is_active { get; set; }
    public bool is_private_session { get; set; }
    public bool is_restricted { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public int? volume_percent { get; set; }
}