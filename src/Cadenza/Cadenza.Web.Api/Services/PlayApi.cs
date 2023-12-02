using Cadenza.Web.Api.Interfaces;
using Cadenza.Web.Api.Settings;

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

    public async Task<List<int>> PlayGenre(string id)
    {
        return await _apiHelper.Get<List<int>>(_settings.PlayGenre, id);
    }

    public async Task<List<int>> PlayGrouping(int id)
    {
        return await _apiHelper.Get<List<int>>(_settings.PlayGrouping, id);
    }

    public async Task<List<int>> PlayTag(string id)
    {
        return await _apiHelper.Get<List<int>>(_settings.PlayTag, id);
    }
}
