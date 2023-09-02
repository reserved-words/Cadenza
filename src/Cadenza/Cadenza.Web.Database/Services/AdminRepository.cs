namespace Cadenza.Web.Database.Services;

internal class AdminRepository : IAdminRepository
{
    private readonly DatabaseApiSettings _settings;
    private readonly IApiHttpHelper _http;

    public AdminRepository(IApiHttpHelper http, IOptions<DatabaseApiSettings> settings)
    {
        _http = http;
        _settings = settings.Value;
    }

    public async Task<List<Grouping>> GetGroupingOptions()
    {
        return await _http.Get<List<Grouping>>(_settings.Endpoints.GroupingOptions);
    }
}
