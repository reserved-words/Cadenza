using Cadenza.Spotify.API.Model.Common;

namespace Cadenza.Spotify.API.Model.Playlists;

public class SpotifyApiPlaylistItemTrack
{
    public string id { get; set; }
    public string uri { get; set; }
    public string name { get; set; }
    public List<SpotifyApiArtist> artists { get; set; }
    public int duration_ms { get; set; }
    public int track_number { get; set; }
}