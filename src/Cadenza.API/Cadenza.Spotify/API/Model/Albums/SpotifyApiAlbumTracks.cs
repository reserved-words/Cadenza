namespace Cadenza.Spotify.API.Model.Albums;

public class SpotifyApiAlbumTracks
{
    public string href { get; set; }
    public List<SpotifyApiAlbumTracksItem> items { get; set; }
    public int limit { get; set; }
    public string next { get; set; } // api url
    public int offset { get; set; }
    public string previous { get; set; } // api url
    public int total { get; set; }
}
