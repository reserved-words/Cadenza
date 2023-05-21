namespace Cadenza.Web.Database.Repositories;

internal class TagRepository : ITagRepository
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;

    public TagRepository(IOptions<DatabaseApiSettings> settings, IApiHttpHelper apiHelper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
    }

    public async Task<List<PlayerItem>> GetTag(string id)
    {
        return await _apiHelper.Get<List<PlayerItem>>(_settings.Tag, id);
    }

}
