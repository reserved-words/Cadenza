namespace Cadenza.Source.Spotify;

public class SpotifyApiPlaylist
{
    public bool collaborative { get; set; }
    public SpotifyApiExternalUrls external_urls { get; set; }
    public string href { get; set; }
    public string id { get; set; }
    public SpotifyApiImage[] images { get; set; }
    public string name { get; set; }
    public SpotifyApiPlaylistOwner owner { get; set; }
    public bool @public { get; set; }
    public string snapshot_id { get; set; }
    public SpotifyApiPlaylistTracks tracks { get; set; }
    public string type { get; set; }
    //public string uri { get; set; }
}
