﻿using Cadenza.Common.Interfaces.Utilities;
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

    public async Task<List<SyncTrackRemovalRequest>> GetRemovalRequests(LibrarySource source)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetRemovalRequests}/{source}";
        return await _http.Get<List<SyncTrackRemovalRequest>>(url);
    }

    public async Task MarkRemovalErrored(SyncTrackRemovalRequest request)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.MarkRemovalErrored}";
        await _http.Post(url, null, request);
    }

    public async Task MarkRemovalDone(SyncTrackRemovalRequest request)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.MarkRemovalDone}";
        await _http.Post(url, null, request);
    }

    public async Task<SyncSourceTrack> GetTrackIdFromSource(int trackId)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetTrackIdFromSource}/{trackId}";
        return await _http.Get<SyncSourceTrack>(url);
    }

    public async Task<List<string>> GetTracksByAlbum(LibrarySource source, int albumId)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetTracksByAlbum}/{source}/{albumId}";
        return await _http.Get<List<string>>(url);
    }

    public async Task<List<string>> GetTracksByArtist(LibrarySource source, int artistId)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetTracksByArtist}/{source}/{artistId}";
        return await _http.Get<List<string>>(url);
    }

    public async Task<List<ItemUpdateRequest>> GetUpdateRequests(LibrarySource source)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.GetUpdateRequests}/{source}";
        return await _http.Get<List<ItemUpdateRequest>>(url);
    }

    public async Task MarkUpdateErrored(LibrarySource source, ItemUpdateRequest request)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.MarkUpdateErrored}/{source}";
        await _http.Post(url, null, request);
    }

    public async Task MarkUpdateDone(LibrarySource source, ItemUpdateRequest request)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.MarkUpdateDone}/{source}";
        await _http.Post(url, null, request);
    }

    public async Task RemoveTracks(LibrarySource source, List<string> idsFromSource)
    {
        var url = $"{_apiSettings.BaseUrl}{_apiSettings.Endpoints.RemoveTracks}/{source}";
        await _http.Post(url, null, idsFromSource);
    }
}
