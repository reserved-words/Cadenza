using Cadenza.Source.Spotify.Api.Interfaces;
using Cadenza.Source.Spotify.Api.Model;
using Cadenza.Source.Spotify.Api.Model.Albums;
using Cadenza.Source.Spotify.Api.Model.Common;
using Cadenza.Source.Spotify.Api.Model.Playlists;
using Cadenza.Source.Spotify.Interfaces;

namespace Cadenza.Source.Spotify.Api;

internal class ApiCaller : IApiCaller
{
    private const string AlbumsUrl = "https://api.spotify.com/v1/me/albums?limit=50&fields=total,next,items";
    private const string PlaylistsUrl = "https://api.spotify.com/v1/me/playlists?limit=50&fields=total,next,items";
    private const string PlaylistTracksUrl = "https://api.spotify.com/v1/playlists/{0}/tracks?limit=50&fields=total,next,items(track)";
    private const string SearchArtistsUrl = "https://api.spotify.com/v1/search?q={0}&type=artist";

    private readonly IApiHelper _api;

    public ApiCaller(IApiHelper api)
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

    public async Task<List<SpotifyApiArtist>> SearchArtists(string searchText)
    {
        var uri = string.Format(SearchArtistsUrl, searchText);
        var data = await GetResponse<SpotifyApiSearchResponse>(uri);
        return data.Artists.items;
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
}