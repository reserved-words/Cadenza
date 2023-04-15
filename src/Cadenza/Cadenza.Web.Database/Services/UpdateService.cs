using Cadenza.Web.Common.Interfaces.Updates;

namespace Cadenza.Web.Database.Services;

internal class UpdateService : IUpdateService
{
    private readonly DatabaseApiSettings _settings;
    private readonly IHttpHelper _http;
    private readonly IDebugLogger _logger;

    public UpdateService(IHttpHelper http, IOptions<DatabaseApiSettings> settings, IDebugLogger logger)
    {
        _http = http;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task RemoveTrack(string trackId)
    {
        await _logger.LogInfo("UpdateService.RemoveTrack");
        await _logger.LogInfo(trackId);
        var data = new TrackRemovalRequest
        {
            TrackId = trackId
        };
        var url = GetApiEndpoint(_settings.Endpoints.RemoveTrack);
        await _logger.LogInfo(url);
        var response = await _http.Delete(url, null, data);
        await _logger.LogInfo(response.StatusCode.ToString());
    }

    public async Task UpdateAlbum(AlbumUpdate update)
    {
        var data = new ItemUpdateRequest
        {
            Id = update.Id,
            Type = update.Type,
            Updates = update.Updates
        };
        var url = GetApiEndpoint(_settings.Endpoints.UpdateAlbum, update.OriginalItem.Source);
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
