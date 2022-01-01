namespace Cadenza.Source.Spotify;

public class SpotifyLibraryApi : ISpotifyLibraryApi
{
    private const string AlbumsUrl = "https://api.spotify.com/v1/me/albums?limit=50&fields=total,next,items";
    private const string PlaylistsUrl = "https://api.spotify.com/v1/me/playlists?limit=50&fields=total,next,items";
    private const string PlaylistTracksUrl = "https://api.spotify.com/v1/playlists/{0}/tracks?limit=50&fields=total,next,items(track)";

    private readonly ISpotifyApi _api;

    public SpotifyLibraryApi(ISpotifyApi api)
    {
        _api = api;
    }

    public async Task<List<SpotifyApiPlaylistItem>> GetPlaylistTracks(string playlistId)
    {
        return await GetListResponse<SpotifyApiPlaylistItem>(string.Format(PlaylistTracksUrl, playlistId));
    }

    public async Task<List<SpotifyApiAlbumsItem>> GetUserAlbums()
    {
        return await GetListResponse<SpotifyApiAlbumsItem>(AlbumsUrl);
    }

    public async Task<List<SpotifyApiPlaylist>> GetUserPlaylists()
    {
        return await GetListResponse<SpotifyApiPlaylist>(PlaylistsUrl);
    }

    private async Task<List<T>> GetListResponse<T>(string uri)
    {
        var response = await _api.Get<SpotifyApiListResponse<T>>(uri);

        var items = new List<T>(response.items);

        while (items.Count() < response.total)
        {
            response = await _api.Get<SpotifyApiListResponse<T>>(response.next);
            items.AddRange(response.items);
        }

        return items;
    }
}