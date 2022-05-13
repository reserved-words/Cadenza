using Cadenza.Source.Spotify.Api.Interfaces;
using Cadenza.Source.Spotify.Api.Internal;
using Cadenza.Source.Spotify.Api.Model;
using Cadenza.Source.Spotify.Api.Model.Albums;
using Cadenza.Source.Spotify.Api.Model.Playlists;
using Cadenza.Source.Spotify.Api.Responses;

namespace Cadenza.Source.Spotify.Api;

internal class UserApi : IUserApi
{
    private const string AlbumsUrl = "https://api.spotify.com/v1/me/albums?limit=50&fields=total,next,items";
    private const string PlaylistsUrl = "https://api.spotify.com/v1/me/playlists?limit=50&fields=total,next,items";
    private const string UserAlbumsUrl = "https://api.spotify.com/v1/me/albums";
    private const string PlaylistFollowersUrl = "https://api.spotify.com/v1/playlists/{0}/followers";

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

    public async Task AddAlbum(string albumId)
    {
        var url = string.Format(UserAlbumsUrl, albumId);
        var data = new { ids = new[] { albumId } };
        var response = await _api.Put(url, data);
        CheckForErrors(response);
    }

    public async Task AddPlaylist(string playlistId)
    {
        var url = string.Format(PlaylistFollowersUrl, playlistId);
        var response = await _api.Put(url);
        CheckForErrors(response);
    }

    public async Task RemoveAlbum(string albumId)
    {
        var url = string.Format(UserAlbumsUrl, albumId);
        var data = new { ids = new[] { albumId } };
        var response = await _api.Delete(url, data);
        CheckForErrors(response);
    }

    public async Task RemovePlaylist(string playlistId)
    {
        var url = string.Format(PlaylistFollowersUrl, playlistId);
        var response = await _api.Delete(url);
        CheckForErrors(response);
    }

    private static void CheckForErrors(ApiResponse response)
    {
        if (response.Error != null)
            throw new Exception($"{response.Error.Status}: {response.Error.Message}");
    }
}
