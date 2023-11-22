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

    public async Task LogPlayedItem(PlaylistId playlistId)
    {
        switch (playlistId.Type)
        {
            case PlaylistType.All:
                await _apiHelper.Post(_settings.LogLibraryRequest);
                break;
            case PlaylistType.Artist:
                await _apiHelper.Post($"{_settings.LogArtistRequest}/{playlistId.Id}");
                break;
            case PlaylistType.Album:
                await _apiHelper.Post($"{_settings.LogAlbumRequest}/{playlistId.Id}");
                break;
            case PlaylistType.Track:
                await _apiHelper.Post($"{_settings.LogTrackRequest}/{playlistId.Id}");
                break;
            case PlaylistType.Genre:
                await _apiHelper.Post($"{_settings.LogGenreRequest}/{playlistId.Id}");
                break;
            case PlaylistType.Grouping:
                await _apiHelper.Post($"{_settings.LogGroupingRequest}/{playlistId.Id}");
                break;
            case PlaylistType.Tag:
                await _apiHelper.Post($"{_settings.LogTagRequest}/{playlistId.Id}");
                break;
            default:
                throw new NotImplementedException($"No method implemented to log played item of type {playlistId.Type}");
        }
    }

}
