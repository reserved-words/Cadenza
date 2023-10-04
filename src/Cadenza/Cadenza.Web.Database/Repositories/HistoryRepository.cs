namespace Cadenza.Web.Database.Repositories;

internal class HistoryRepository : IPlaylistHistory
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;

    public HistoryRepository(IOptions<DatabaseApiSettings> settings, IApiHttpHelper apiHelper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
    }

    public async Task<List<RecentAlbum>> GetRecentAlbums(int maxItems)
    {
        var url = $"{_settings.GetRecentAlbums}/{maxItems}";
        return await _apiHelper.Get<List<RecentAlbum>>(url);
    }

    public async Task<List<string>> GetRecentTags(int maxItems)
    {
        var url = $"{_settings.GetRecentTags}/{maxItems}";
        return await _apiHelper.Get<List<string>>(url);
    }

    public async Task LogPlayedItem(PlaylistId playlistId)
    {
        switch (playlistId.Type)
        {
            case PlaylistType.All:
                await _apiHelper.Post(_settings.LogLibraryPlay);
                break;
            case PlaylistType.Artist:
                await _apiHelper.Post($"{_settings.LogArtistPlay}/{playlistId.Id}");
                break;
            case PlaylistType.Album:
                await _apiHelper.Post($"{_settings.LogAlbumPlay}/{playlistId.Id}");
                break;
            case PlaylistType.Track:
                await _apiHelper.Post($"{_settings.LogTrackPlay}/{playlistId.Id}");
                break;
            case PlaylistType.Genre:
                await _apiHelper.Post($"{_settings.LogGenrePlay}/{playlistId.Id}");
                break;
            case PlaylistType.Grouping:
                await _apiHelper.Post($"{_settings.LogGroupingPlay}/{playlistId.Id}");
                break;
            case PlaylistType.Tag:
                await _apiHelper.Post($"{_settings.LogTagPlay}/{playlistId.Id}");
                break;
            default:
                throw new NotImplementedException($"No method implemented to log played item of type {playlistId.Type}");
        }
    }

}
