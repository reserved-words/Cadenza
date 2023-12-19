namespace Cadenza.Web.Api.Services;

internal class PlayApi : IPlayApi
{
    private readonly ApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;

    public PlayApi(IOptions<ApiSettings> settings, IApiHttpHelper apiHelper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
    }

    public async Task<List<int>> PlayAll()
    {
        return await _apiHelper.Get<List<int>>(_settings.PlayTracks);
    }

    public async Task<List<int>> PlayAlbum(int id)
    {
        return await _apiHelper.Get<List<int>>(_settings.PlayAlbum, id);
    }

    public async Task<List<int>> PlayArtist(int id)
    {
        return await _apiHelper.Get<List<int>>(_settings.PlayArtist, id);
    }

    public async Task<List<int>> PlayGenre(string grouping, string genre)
    {
        return await _apiHelper.Get<List<int>>($"{_settings.PlayGenre}?grouping={grouping}&genre={genre}");
    }

    public async Task<List<int>> PlayGrouping(string grouping)
    {
        return await _apiHelper.Get<List<int>>(_settings.PlayGrouping, grouping);
    }

    public async Task<List<int>> PlayTag(string id)
    {
        return await _apiHelper.Get<List<int>>(_settings.PlayTag, id);
    }
}
