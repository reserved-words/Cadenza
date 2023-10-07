namespace Cadenza.Web.Database.Repositories;

internal class SearchRepository : ISearchRepository
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;
    private readonly IViewModelMapper _mapper;

    public SearchRepository(IOptions<DatabaseApiSettings> settings, IApiHttpHelper apiHelper, IViewModelMapper mapper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
        _mapper = mapper;
    }

    public async Task<List<PlayerItemVM>> GetSearchAlbums()
    {
        var items = await _apiHelper.Get<List<PlayerItemDTO>>(_settings.SearchAlbums);
        return items.Select(i => _mapper.Map(i)).ToList();
    }

    public async Task<List<PlayerItemVM>> GetArtists()
    {
        var items = await _apiHelper.Get<List<PlayerItemDTO>>(_settings.SearchArtists);
        return items.Select(i => _mapper.Map(i)).ToList();
    }

    public async Task<List<PlayerItemVM>> GetGenres()
    {
        var items = await _apiHelper.Get<List<PlayerItemDTO>>(_settings.SearchGenres);
        return items.Select(i => _mapper.Map(i)).ToList();
    }

    public async Task<List<PlayerItemVM>> GetGroupings()
    {
        var items = await _apiHelper.Get<List<PlayerItemDTO>>(_settings.SearchGroupings);
        return items.Select(i => _mapper.Map(i)).ToList();
    }

    public async Task<List<PlayerItemVM>> GetSearchPlaylists()
    {
        var items = await _apiHelper.Get<List<PlayerItemDTO>>(_settings.SearchPlaylists);
        return items.Select(i => _mapper.Map(i)).ToList();
    }

    public async Task<List<PlayerItemVM>> GetTags()
    {
        var items = await _apiHelper.Get<List<PlayerItemDTO>>(_settings.SearchTags);
        return items.Select(i => _mapper.Map(i)).ToList();
    }

    public async Task<List<PlayerItemVM>> GetTracks()
    {
        var items = await _apiHelper.Get<List<PlayerItemDTO>>(_settings.SearchTracks);
        return items.Select(i => _mapper.Map(i)).ToList();
    }
}
