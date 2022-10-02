using Cadenza.Web.Common.Interfaces.Updates;

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

    public async Task UpdateAlbum(AlbumUpdate update)
    {
        var data = new ItemUpdates
        {
            Id = update.Id,
            Type = update.Type,
            Name = update.Name,
            Updates = update.Updates
        };
        var url = GetApiEndpoint(_settings.Endpoints.UpdateAlbum, update.OriginalItem.Source);
        await _http.Post(url, null, data);
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
        var url = GetApiEndpoint(_settings.Endpoints.UpdateArtist);
        await _http.Post(url, null, data);
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
        var url = GetApiEndpoint(_settings.Endpoints.UpdateTrack, update.OriginalItem.Source);
        await _http.Post(url, null, data);
    }

    private string GetApiEndpoint(string endpoint, LibrarySource? source = null)
    {
        return source.HasValue
            ? $"{_settings.BaseUrl}{endpoint}/{source.Value}"
            : $"{_settings.BaseUrl}{endpoint}";
    }
}
