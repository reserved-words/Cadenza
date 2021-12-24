namespace Cadenza.Source.Spotify;

public class SpotifyLibraryApi : ISpotifyLibraryApi
{
    private const string AlbumsUrl = "https://api.spotify.com/v1/me/albums?limit=50";
    private const string PlaylistsUrl = "https://api.spotify.com/v1/me/playlists?limit=50";
    private const string PlaylistTracksUrl = "https://api.spotify.com/v1/playlists/{0}/tracks?limit=50&fields=items(track)";

    private readonly ISpotifyApi _api;

    public SpotifyLibraryApi(ISpotifyApi api)
    {
        _api = api;
    }

    public async Task<SpotifyApiPlaylistItemsResponse> GetPlaylistTracks(string playlistId)
    {
        return await _api.Get<SpotifyApiPlaylistItemsResponse>(string.Format(PlaylistTracksUrl, playlistId));
    }

    public async Task<SpotifyApiAlbumsResponse> GetUserAlbums()
    {
        return await _api.Get<SpotifyApiAlbumsResponse>(AlbumsUrl);
    }

    public async Task<SpotifyApiPlaylistsResponse> GetUserPlaylists()
    {
        return await _api.Get<SpotifyApiPlaylistsResponse>(PlaylistsUrl);
    }

}