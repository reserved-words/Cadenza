namespace Cadenza.Web.Database.Repositories;

internal class SearchRepository : ISearchRepository
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHelper _apiHelper;

    public SearchRepository(IOptions<DatabaseApiSettings> settings, IApiHelper apiHelper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
    }

    public async Task<List<PlayerItem>> GetSearchAlbums()
    {
        return await _apiHelper.Get<List<PlayerItem>>(_settings.SearchAlbums);
    }

    public async Task<List<PlayerItem>> GetSearchArtists()
    {
        return await _apiHelper.Get<List<PlayerItem>>(_settings.SearchArtists);
    }

    public async Task<List<PlayerItem>> GetSearchPlaylists()
    {
        return await _apiHelper.Get<List<PlayerItem>>(_settings.SearchPlaylists);
    }

    public async Task<List<PlayerItem>> GetSearchTracks()
    {
        return await _apiHelper.Get<List<PlayerItem>>(_settings.SearchTracks);
    }

    public async Task<List<PlayerItem>> GetSearchGenres()
    {
        return await _apiHelper.Get<List<PlayerItem>>(_settings.SearchGenres);
    }

    public async Task<List<PlayerItem>> GetSearchGroupings()
    {
        return await _apiHelper.Get<List<PlayerItem>>(_settings.SearchGroupings);
    }
}
