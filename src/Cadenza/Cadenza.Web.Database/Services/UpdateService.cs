﻿using Cadenza.Web.Common.Interfaces.Updates;

namespace Cadenza.Web.Database.Services;

internal class UpdateService : IUpdateService
{
    private readonly DatabaseApiSettings _settings;
    private readonly IHttpHelper _http;

    public UpdateService(IHttpHelper http, IOptions<DatabaseApiSettings> settings)
    {
        _http = http;
        _settings = settings.Value;
    }

    public async Task RemoveTrack(int trackId)
    {
        var data = new TrackRemovalRequest
        {
            TrackId = trackId
        };
        var url = GetApiEndpoint(_settings.Endpoints.RemoveTrack);
        await _http.Delete(url, null, data);
    }

    public async Task UpdateAlbum(AlbumUpdate update)
    {
        var data = new ItemUpdateRequest
        {
            Id = update.Id,
            Type = update.Type,
            Updates = update.Updates
        };
        var url = GetApiEndpoint(_settings.Endpoints.UpdateAlbum);
        await _http.Post(url, null, data);
    }

    public async Task UpdateArtist(ArtistUpdate update)
    {
        var data = new ItemUpdateRequest
        {
            Id = update.Id,
            Type = update.Type,
            Updates = update.Updates
        };
        var url = GetApiEndpoint(_settings.Endpoints.UpdateArtist);
        await _http.Post(url, null, data);
    }

    public async Task UpdateTrack(TrackUpdate update)
    {
        var data = new ItemUpdateRequest
        {
            Id = update.Id,
            Type = update.Type,
            Updates = update.Updates
        };
        var url = GetApiEndpoint(_settings.Endpoints.UpdateTrack);
        await _http.Post(url, null, data);
    }

    private string GetApiEndpoint(string endpoint)
    {
        return $"{_settings.BaseUrl}{endpoint}";
    }
}
