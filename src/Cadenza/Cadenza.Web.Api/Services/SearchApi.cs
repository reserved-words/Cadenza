namespace Cadenza.Web.Api.Services;

internal class SearchApi : ISearchApi
{
    private readonly ApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;
    private readonly IViewModelMapper _mapper;

    public SearchApi(IOptions<ApiSettings> settings, IApiHttpHelper apiHelper, IViewModelMapper mapper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
        _mapper = mapper;
    }

    public async Task<List<SearchItemVM>> GetAlbums()
    {
        var items = await _apiHelper.Get<List<SearchItemDTO>>(_settings.SearchAlbums);
        return items.Select(i => _mapper.Map(i)).ToList();
    }

    public async Task<List<SearchItemVM>> GetArtists()
    {
        var items = await _apiHelper.Get<List<SearchItemDTO>>(_settings.SearchArtists);
        return items.Select(i => _mapper.Map(i)).ToList();
    }

    public async Task<List<SearchItemVM>> GetGenres()
    {
        var items = await _apiHelper.Get<List<SearchItemDTO>>(_settings.SearchGenres);
        return items.Select(i => _mapper.Map(i)).ToList();
    }

    public async Task<List<SearchItemVM>> GetGroupings()
    {
        var items = await _apiHelper.Get<List<SearchItemDTO>>(_settings.SearchGroupings);
        return items.Select(i => _mapper.Map(i)).ToList();
    }

    public async Task<List<SearchItemVM>> GetTags()
    {
        var items = await _apiHelper.Get<List<SearchItemDTO>>(_settings.SearchTags);
        return items.Select(i => _mapper.Map(i)).ToList();
    }

    public async Task<List<SearchItemVM>> GetTracks()
    {
        var items = await _apiHelper.Get<List<SearchItemDTO>>(_settings.SearchTracks);
        return items.Select(i => _mapper.Map(i)).ToList();
    }
}
