namespace Cadenza.Web.Database.Repositories;

internal class PlayTrackRepository : IPlayTrackRepository
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHelper _apiHelper;

    public PlayTrackRepository(IOptions<DatabaseApiSettings> settings, IApiHelper apiHelper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
    }

    public async Task<List<PlayTrack>> PlayAll()
    {
        return await _apiHelper.Get<List<PlayTrack>>(_settings.PlayTracks);
    }

    public async Task<List<PlayTrack>> PlayAlbum(int id)
    {
        return await _apiHelper.Get<List<PlayTrack>>(_settings.PlayAlbum, id);
    }

    public async Task<List<PlayTrack>> PlayArtist(string id)
    {
        return await _apiHelper.Get<List<PlayTrack>>(_settings.PlayArtist, id);
    }

    public async Task<List<PlayTrack>> PlayGenre(string id)
    {
        return await _apiHelper.Get<List<PlayTrack>>(_settings.PlayGenre, id);
    }

    public async Task<List<PlayTrack>> PlayGrouping(Grouping id)
    {
        return await _apiHelper.Get<List<PlayTrack>>(_settings.PlayGrouping, id.ToString());
    }

    public async Task<List<PlayTrack>> PlayTag(string id)
    {
        return await _apiHelper.Get<List<PlayTrack>>(_settings.PlayTag, id);
    }
}
