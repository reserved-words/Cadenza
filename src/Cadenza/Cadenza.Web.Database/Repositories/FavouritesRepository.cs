namespace Cadenza.Web.Database.Repositories;

internal class FavouritesRepository : IFavouritesService
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;

    public FavouritesRepository(IOptions<DatabaseApiSettings> settings, IApiHttpHelper apiHelper)
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
