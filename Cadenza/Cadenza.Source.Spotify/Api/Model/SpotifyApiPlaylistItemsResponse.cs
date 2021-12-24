namespace Cadenza.Source.Spotify;

public class SpotifyApiPlaylistItemsResponse
{
    public string href { get; set; }
    public SpotifyApiPlaylistItem[] items { get; set; }
    public int limit { get; set; }
    public string next { get; set; }
    public int offset { get; set; }
    public string previous { get; set; }
    public int total { get; set; }
}

public class SpotifyApiPlaylistItem
{
    public SpotifyApiPlaylistItemTrack track { get; set; }
}

public class SpotifyApiPlaylistItemTrack
{
    public string id { get; set; }
    // public string uri { get; set; }
    public string name { get; set; }
    public List<SpotifyApiArtist> artists { get; set; }
    public int duration_ms { get; set; }
    public int track_number { get; set; }
}