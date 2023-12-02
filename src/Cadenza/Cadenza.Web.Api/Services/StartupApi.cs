using Cadenza.Web.Api.Interfaces;
using Cadenza.Web.Api.Settings;

namespace Cadenza.Web.Api.Services;

internal class StartupApi : IStartupApi
{
    private readonly IApiHttpHelper _httpHelper;
    private readonly IOptions<ApiSettings> _apiSettings;

    public StartupApi(IApiHttpHelper httpHelper, IOptions<ApiSettings> apiSettings)
    {
        _httpHelper = httpHelper;
        _apiSettings = apiSettings;
    }

    public async Task Connect()
    {
        await _httpHelper.Get(_apiSettings.Value.Endpoints.Connect);
    }
}
