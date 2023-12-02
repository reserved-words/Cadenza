using Cadenza.Web.Api.Interfaces;
using Cadenza.Web.Api.Settings;

namespace Cadenza.Web.Api.Services;

internal class FavouritesApi : IFavouritesApi
{
    private readonly ApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;

    public FavouritesApi(IOptions<ApiSettings> settings, IApiHttpHelper apiHelper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
    }

    public async Task Favourite(int trackId)
    {
        await _apiHelper.Post(_settings.LoveTrack, new UpdateLovedTrackDTO { TrackId = trackId });
    }

    public async Task Unfavourite(int trackId)
    {
        await _apiHelper.Post(_settings.UnloveTrack, new UpdateLovedTrackDTO { TrackId = trackId });
    }
}
