namespace Cadenza.Web.Database.Repositories;

internal class TrackRepository : ITrackRepository
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHelper _apiHelper;

    public TrackRepository(IOptions<DatabaseApiSettings> settings, IApiHelper apiHelper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
    }

    public async Task<TrackFull> GetTrack(string id)
    {
        return await _apiHelper.Get<TrackFull>(_settings.Track, id);
    }

}
