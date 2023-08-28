using Cadenza.Web.Common.Interfaces.Updates;

namespace Cadenza.Web.Database.Services;

internal class UpdateService : IUpdateService
{
    private readonly DatabaseApiSettings _settings;
    private readonly IApiHttpHelper _http;

    public UpdateService(IApiHttpHelper http, IOptions<DatabaseApiSettings> settings)
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
        await _http.Delete(_settings.Endpoints.RemoveTrack, data);
    }

    public async Task UpdateAlbum(AlbumUpdate update)
    {
        var data = new ItemUpdateRequest
        {
            Id = update.Id,
            Type = update.Type,
            Updates = update.Updates
        };
        await _http.Post(_settings.Endpoints.UpdateAlbum, data);
    }

    public async Task UpdateArtist(ArtistUpdate update)
    {
        var data = new ItemUpdateRequest
        {
            Id = update.Id,
            Type = update.Type,
            Updates = update.Updates
        };
        await _http.Post(_settings.Endpoints.UpdateArtist, data);
    }

    public async Task UpdateTrack(TrackUpdate update)
    {
        var data = new ItemUpdateRequest
        {
            Id = update.Id,
            Type = update.Type,
            Updates = update.Updates
        };
        await _http.Post(_settings.Endpoints.UpdateTrack, data);
    }
}
