using Cadenza.Common.Interfaces.Utilities;
using Microsoft.Extensions.Options;

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

    public async Task AddTrack(LibrarySource source, SyncTrack track)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.AddTrack}/{source}";
        await _http.Post(url, null, track);
    }

    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetAllTracks}/{source}";
        return await _http.Get<List<string>>(url);
    }

    public async Task<List<string>> GetTracksByAlbum(LibrarySource source, string albumId)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetTracksByAlbum}/{source}/{albumId}";
        return await _http.Get<List<string>>(url);
    }

    public async Task<List<string>> GetTracksByArtist(LibrarySource source, string artistId)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetTracksByArtist}/{source}/{artistId}";
        return await _http.Get<List<string>>(url);
    }

    public async Task<List<ItemUpdateRequest>> GetUpdateRequests(LibrarySource source)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetUpdateRequests}/{source}";
        return await _http.Get<List<ItemUpdateRequest>>(url);
    }

    public async Task MarkErrored(LibrarySource source, ItemUpdateRequest request)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.MarkErrored}/{source}";
        await _http.Post(url, null, request);
    }

    public async Task MarkUpdated(LibrarySource source, ItemUpdateRequest request)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.MarkUpdated}/{source}";
        await _http.Post(url, null, request);
    }

    public async Task RemoveTracks(LibrarySource source, List<string> ids)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.RemoveTracks}/{source}";
        await _http.Post(url, null, ids);
    }
}
