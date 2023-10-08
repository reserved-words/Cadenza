using Cadenza.Common.Enums;
using Microsoft.Extensions.Options;

namespace Cadenza.SyncService.Services;

internal class DatabaseRepository : IDatabaseRepository
{
    private readonly ISyncHttpHelper _http;
    private readonly DatabaseApiSettings _apiSettings;

    public DatabaseRepository(ISyncHttpHelper http, IOptions<DatabaseApiSettings> apiSettings)
    {
        _http = http;
        _apiSettings = apiSettings.Value;
    }

    public async Task AddTrack(LibrarySource source, SyncTrackDTO track)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.AddTrack}/{source}";
        await _http.Post(url, track);
    }

    public async Task<List<string>> GetAllTracks(LibrarySource source)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetAllTracks}/{source}";
        return await _http.Get<List<string>>(url);
    }

    public async Task<List<SyncTrackRemovalRequestDTO>> GetRemovalRequests(LibrarySource source)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetRemovalRequests}/{source}";
        return await _http.Get<List<SyncTrackRemovalRequestDTO>>(url);
    }

    public async Task MarkRemovalErrored(SyncTrackRemovalRequestDTO request)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.MarkRemovalErrored}";
        await _http.Post(url, request);
    }

    public async Task MarkRemovalDone(SyncTrackRemovalRequestDTO request)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.MarkRemovalDone}";
        await _http.Post(url, request);
    }

    public async Task<SyncSourceTrackDTO> GetTrackIdFromSource(int trackId)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetTrackIdFromSource}/{trackId}";
        return await _http.Get<SyncSourceTrackDTO>(url);
    }

    public async Task<List<string>> GetTracksByAlbum(int albumId)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetTracksByAlbum}/{albumId}";
        return await _http.Get<List<string>>(url);
    }

    public async Task<List<string>> GetTracksByArtist(int artistId)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetTracksByArtist}/{artistId}";
        return await _http.Get<List<string>>(url);
    }

    public async Task<List<ItemUpdateRequestDTO>> GetUpdateRequests(LibrarySource source)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetUpdateRequests}/{source}";
        return await _http.Get<List<ItemUpdateRequestDTO>>(url);
    }

    public async Task MarkUpdateErrored(ItemUpdateRequestDTO request)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.MarkUpdateErrored}";
        await _http.Post(url, request);
    }

    public async Task MarkUpdateDone(ItemUpdateRequestDTO request)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.MarkUpdateDone}";
        await _http.Post(url, request);
    }

    public async Task RemoveTracks(LibrarySource source, List<string> idsFromSource)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.RemoveTracks}/{source}";
        await _http.Post(url, idsFromSource);
    }
}
