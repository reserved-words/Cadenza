namespace Cadenza.Web.Database.Repositories;

internal class HistoryRepository : IPlaylistHistory
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;
    private readonly IViewModelMapper _mapper;

    public HistoryRepository(IOptions<DatabaseApiSettings> settings, IApiHttpHelper apiHelper, IViewModelMapper mapper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
        _mapper = mapper;
    }

    public async Task<List<RecentAlbumVM>> GetRecentAlbums(int maxItems)
    {
        var url = $"{_settings.RecentAlbumRequests}/{maxItems}";
        var items = await _apiHelper.Get<List<RecentAlbumDTO>>(url);
        return items.Select(i => _mapper.Map(i)).ToList();
    }

    public async Task<List<string>> GetRecentTags(int maxItems)
    {
        var url = $"{_settings.RecentTagRequests}/{maxItems}";
        return await _apiHelper.Get<List<string>>(url);
    }
}
