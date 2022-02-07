using Cadenza.API.Core;
using Cadenza.API.Wrapper.Core;
using Cadenza.Domain;
using Cadenza.Library;
using Cadenza.Utilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Cadenza.Spotify.Repositories;

internal class SpotifySearchRepository : ISourceSearchRepository
{
    private readonly IOptions<ApiSettings> _settings;
    private readonly IHttpHelper _http;

    public SpotifySearchRepository(IHttpHelper http, IOptions<ApiSettings> settings)
    {
        _http = http;
        _settings = settings;
    }

    public LibrarySource Source => LibrarySource.Spotify;

    public async Task<ListResponse<SearchableItem>> GetSearchAlbums(int page, int limit)
    {
        var response = await _http.Get(GetApiEndpoint(ApiEndpoints.Spotify.SearchAlbums, page, limit));
        return await response.Content.ReadFromJsonAsync<ListResponse<SearchableItem>>();
    }

    public async Task<ListResponse<SearchableItem>> GetSearchArtists(int page, int limit)
    {
        var response = await _http.Get(GetApiEndpoint(ApiEndpoints.Spotify.SearchArtists, page, limit));
        return await response.Content.ReadFromJsonAsync<ListResponse<SearchableItem>>();
    }

    public async Task<ListResponse<SearchableItem>> GetSearchPlaylists(int page, int limit)
    {
        var response = await _http.Get(GetApiEndpoint(ApiEndpoints.Spotify.SearchPlaylists, page, limit));
        return await response.Content.ReadFromJsonAsync<ListResponse<SearchableItem>>();
    }

    public async Task<ListResponse<SearchableItem>> GetSearchTracks(int page, int limit)
    {
        var response = await _http.Get(GetApiEndpoint(ApiEndpoints.Spotify.SearchTracks, page, limit));
        return await response.Content.ReadFromJsonAsync<ListResponse<SearchableItem>>();
    }

    private string GetApiEndpoint(string endpoint, int page, int limit)
    {
        return $"{_settings.Value.BaseUrl}{endpoint}?page={page}&limit={limit}";
    }
}