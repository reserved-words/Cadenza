using Cadenza.Domain.Enums;
using Cadenza.Domain.Models;
using Cadenza.Domain.Models.Track;
using Cadenza.SyncService.Interfaces;
using Cadenza.SyncService.Settings;
using Cadenza.Utilities.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Cadenza.SyncService.Repositories;

internal class DatabaseRepository : IDatabaseRepository
{
    private readonly IHttpHelper _http;
    private readonly DatabaseApiSettings _apiSettings;

    public DatabaseRepository(IHttpHelper http, IOptions<DatabaseApiSettings> apiSettings)
    {
        _http = http;
        _apiSettings = apiSettings.Value;
    }

    public async Task AddTrack(LibrarySource source, TrackFull track)
    {
        var data = new { source, track };
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.AddTrack}";
        await _http.Post(url, null, data);
    }

    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetAllTracks}";
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<List<string>>();
    }

    public async Task<List<string>> GetTracksByAlbum(LibrarySource source, string albumId)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetTracksByAlbum}";
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<List<string>>();
    }

    public async Task<List<string>> GetTracksByArtist(LibrarySource source, string artistId)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetTracksByArtist}";
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<List<string>>();
    }

    public async Task<List<ItemUpdates>> GetUpdates(LibrarySource source)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetUpdates}";
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<List<ItemUpdates>>();
    }

    public async Task MarkUpdated(LibrarySource source, LibraryItemType itemType, string id)
    {
        var data = new { source, itemType, id };
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.MarkUpdated}";
        await _http.Post(url, null, data);
    }

    public async Task RemoveTrack(LibrarySource source, string id)
    {
        var data = new { source, id };
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.RemoveTrack}";
        await _http.Post(url, null, data);
    }
}
