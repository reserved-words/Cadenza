using Cadenza.Domain.Enums;
using Cadenza.Domain.Model.Update;
using Cadenza.Domain.Model.Updates;
using Cadenza.Utilities.Interfaces;
using Cadenza.Web.Common.Interfaces;

namespace Cadenza.Web.Database.Services;

internal class UpdateService : IUpdateService
{

    private readonly IApiRepositorySettings _settings;
    private readonly IHttpHelper _http;

    public UpdateService(IHttpHelper http, IApiRepositorySettings settings) 
    {
        _http = http;
        _settings = settings;
    }

    public async Task UpdateAlbum(AlbumUpdate update)
    {
        var data = new ItemUpdates
        {
            Id = update.Id,
            Type = update.Type,
            Name = update.Name,
            Updates = update.Updates
        };
        var url = GetApiEndpoint(_settings.UpdateAlbum, update.OriginalItem.Source);
        var response = await _http.Post(url, null, data);
    }

    public async Task UpdateArtist(ArtistUpdate update)
    {
        var data = new ItemUpdates
        {
            Id = update.Id,
            Type = update.Type,
            Name = update.Name,
            Updates = update.Updates
        };
        var url = GetApiEndpoint(_settings.UpdateArtist);
        var response = await _http.Post(url, null, data);
    }

    public async Task UpdateTrack(TrackUpdate update)
    {
        var data = new ItemUpdates
        {
            Id = update.Id,
            Type = update.Type,
            Name = update.Name,
            Updates = update.Updates
        };
        var url = GetApiEndpoint(_settings.UpdateTrack, update.OriginalItem.Source);
        var response = await _http.Post(url, null, data);
    }

    private string GetApiEndpoint(string endpoint, LibrarySource? source = null)
    {
        return source.HasValue
            ? $"{_settings.BaseUrl}{endpoint}/{source.Value}"
            : $"{_settings.BaseUrl}{endpoint}";
    }
}
