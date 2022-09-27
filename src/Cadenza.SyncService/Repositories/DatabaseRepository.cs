using Cadenza.Domain.Enums;
using Cadenza.Domain.Model.Track;
using Cadenza.Domain.Model.Updates;
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
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.AddTrack}/{source}";
        await _http.Post(url, null, track);
    }

    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetAllTracks}/{source}";
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<List<string>>();
    }

    public async Task<List<string>> GetTracksByAlbum(LibrarySource source, string albumId)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetTracksByAlbum}/{source}/{albumId}";
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<List<string>>();
    }

    public async Task<List<string>> GetTracksByArtist(LibrarySource source, string artistId)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetTracksByArtist}/{source}/{artistId}";
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<List<string>>();
    }

    public async Task<List<ItemUpdates>> GetUpdates(LibrarySource source)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetUpdates}/{source}";
        var response = await _http.Get(url);
        return await response.Content.ReadFromJsonAsync<List<ItemUpdates>>();
    }

    public async Task MarkUpdated(LibrarySource source, ItemUpdates update)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.MarkUpdated}/{source}";
        await _http.Post(url, null, update);
    }

    public async Task RemoveTracks(LibrarySource source, List<string> ids)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.RemoveTracks}/{source}";
        var response = await _http.Post(url, null, ids);
    }
}
