namespace Cadenza.Web.Database.Repositories;

internal class SearchRepository : ISearchRepository
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;

    public SearchRepository(IOptions<DatabaseApiSettings> settings, IApiHttpHelper apiHelper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
    }

    public async Task<List<PlayerItem>> GetSearchAlbums()
    {
        return await _apiHelper.Get<List<PlayerItem>>(_settings.SearchAlbums);
    }

    public async Task<List<PlayerItem>> GetArtists()
    {
        return await _apiHelper.Get<List<PlayerItem>>(_settings.SearchArtists);
    }

    public async Task<List<PlayerItem>> GetGenres()
    {
        return await _apiHelper.Get<List<PlayerItem>>(_settings.SearchGenres);
    }

    public async Task<List<PlayerItem>> GetGroupings()
    {
        return await _apiHelper.Get<List<PlayerItem>>(_settings.SearchGroupings);
    }

    public async Task<List<PlayerItem>> GetSearchPlaylists()
    {
        return await _apiHelper.Get<List<PlayerItem>>(_settings.SearchPlaylists);
    }

    public async Task<List<PlayerItem>> GetTags()
    {
        return await _apiHelper.Get<List<PlayerItem>>(_settings.SearchTags);
    }

    public async Task<List<PlayerItem>> GetTracks()
    {
        return await _apiHelper.Get<List<PlayerItem>>(_settings.SearchTracks);
    }
}
