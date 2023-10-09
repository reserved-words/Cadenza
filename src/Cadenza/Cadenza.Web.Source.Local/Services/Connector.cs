namespace Cadenza.Web.Source.Local.Services;

internal class Connector : ILocalSourceConnector
{
    private readonly ILocalHttpHelper _httpHelper;
    private readonly IOptions<LocalApiSettings> _apiSettings;

    public Connector(ILocalHttpHelper httpHelper, IOptions<LocalApiSettings> apiSettings)
    {
        _httpHelper = httpHelper;
        _apiSettings = apiSettings;
    }

    public async Task Connect()
    {
        await _httpHelper.Get(_apiSettings.Value.Endpoints.Connect);
    }
}
