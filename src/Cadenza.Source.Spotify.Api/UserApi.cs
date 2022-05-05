using Cadenza.Source.Spotify.Api.Interfaces;
using Cadenza.Source.Spotify.Api.Internal;
using Cadenza.Source.Spotify.Api.Model;
using Cadenza.Source.Spotify.Api.Model.Albums;
using Cadenza.Source.Spotify.Api.Model.Playlists;

namespace Cadenza.Source.Spotify.Api;

internal class UserApi : IUserApi
{
    private const string AlbumsUrl = "https://api.spotify.com/v1/me/albums?limit=50&fields=total,next,items";
    private const string PlaylistsUrl = "https://api.spotify.com/v1/me/playlists?limit=50&fields=total,next,items";
    private const string SaveAlbumsUrl = "https://api.spotify.com/v1/me/albums";
    private const string FollowPlaylistUrl = "https://api.spotify.com/v1/playlists/{0}/followers";

    private readonly IApiHelper _api;

    public UserApi(IApiHelper api)
    {
        _api = api;
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
        var apiResponse = await _api.Get<SpotifyApiListResponse<T>>(uri);

        if (apiResponse.Error != null)
        {
            var ex = new Exception("API error");
            ex.Data.Add("Message", apiResponse.Error.Error.Message);
            ex.Data.Add("Status", apiResponse.Error.Error.Status);
            throw ex;
        }

        var response = apiResponse.Data;

        if (response == null)
            return new List<T>();

        var items = new List<T>(response.items);

        while (items.Count() < response.total)
        {
            apiResponse = await _api.Get<SpotifyApiListResponse<T>>(response.next);
            response = apiResponse.Data;

            if (response == null)
                return items;

            items.AddRange(response.items);
        }

        return items;
    }

    private async Task<T> GetResponse<T>(string uri) where T : class
    {
        var apiResponse = await _api.Get<T>(uri);

        if (apiResponse.Error != null)
        {
            var ex = new Exception("API error");
            ex.Data.Add("Message", apiResponse.Error.Error.Message);
            ex.Data.Add("Status", apiResponse.Error.Error.Status);
            throw ex;
        }

        return apiResponse.Data;
    }

    public async Task AddAlbum(string albumId)
    {
        var url = string.Format(SaveAlbumsUrl, albumId);
        var data = new { ids = new[] { albumId } };
        await _api.Put(url, data);
    }

    public async Task AddPlaylist(string playlistId)
    {
        var url = string.Format(FollowPlaylistUrl, playlistId);
        await _api.Put(url);
    }
}
