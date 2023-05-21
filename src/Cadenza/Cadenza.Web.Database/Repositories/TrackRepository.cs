namespace Cadenza.Web.Database.Repositories;

internal class TrackRepository : ITrackRepository
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;

    public TrackRepository(IOptions<DatabaseApiSettings> settings, IApiHttpHelper apiHelper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
    }

    public async Task<TrackFull> GetTrack(int id)
    {
        return await _apiHelper.Get<TrackFull>(_settings.Track, id);
    }

}
