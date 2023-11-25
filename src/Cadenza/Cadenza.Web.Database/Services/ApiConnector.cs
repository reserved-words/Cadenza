namespace Cadenza.Web.Database.Services;

internal class ApiConnector : IApiConnector
{
    private readonly IApiHttpHelper _httpHelper;
    private readonly IOptions<DatabaseApiSettings> _apiSettings;

    public ApiConnector(IApiHttpHelper httpHelper, IOptions<DatabaseApiSettings> apiSettings)
    {
        _httpHelper = httpHelper;
        _apiSettings = apiSettings;
    }

    public async Task Connect()
    {
        var connectionUrl = _apiSettings.Value.Endpoints.Connect;
        await _httpHelper.Get(connectionUrl);
    }
}
