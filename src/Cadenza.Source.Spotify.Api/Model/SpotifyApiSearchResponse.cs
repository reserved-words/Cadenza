using Cadenza.Source.Spotify.Api.Model.Artist;
using Cadenza.Source.Spotify.Api.Model.Playlists;

namespace Cadenza.Source.Spotify.Api.Model;

public class SpotifyApiSearchResponse
{
    public SpotifyApiListResponse<SpotifyApiArtist> Artists { get; set; }
    public SpotifyApiListResponse<SpotifyApiPlaylist> Playlists { get; set; }
    // Tracks
    // Albums
    // Shows
    // Episodes
}
