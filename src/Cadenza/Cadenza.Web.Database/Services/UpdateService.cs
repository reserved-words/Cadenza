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

    public async Task RemoveTrack(int trackId)
    {
        var data = new TrackRemovalRequest
        {
            TrackId = trackId
        };
        var url = GetApiEndpoint(_settings.Endpoints.RemoveTrack);
        var response = await _http.Delete(url, null, data);
        await ValidateResponse(response);
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
        var response = await _http.Post(url, null, data);
        await ValidateResponse(response);
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
        var response = await _http.Post(url, null, data);
        await ValidateResponse(response);
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
        var response = await _http.Post(url, null, data);
        await ValidateResponse(response);
    }

    private string GetApiEndpoint(string endpoint)
    {
        return $"{_settings.BaseUrl}{endpoint}";
    }

    private async Task ValidateResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return;

        var responseContent = await response.Content.ReadFromJsonAsync<ApiError>();

        var errorMessage = responseContent?.Message ?? response.StatusCode.ToString();

        throw new Exception(errorMessage);
    }
}
