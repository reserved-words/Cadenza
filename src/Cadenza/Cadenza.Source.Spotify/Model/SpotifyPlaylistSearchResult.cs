namespace Cadenza.Source.Spotify.Model;

public class SpotifyPlaylistSearchResult
{
    public SpotifyPlaylist Playlist { get; set; }
    public List<SpotifyTrack> Tracks { get; set; }
}