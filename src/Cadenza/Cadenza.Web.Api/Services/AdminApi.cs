using Cadenza.Web.Api.Interfaces;
using Cadenza.Web.Api.Settings;

namespace Cadenza.Web.Api.Services;

internal class AdminApi : IAdminApi
{
    private readonly ApiSettings _settings;
    private readonly IApiHttpHelper _http;
    private readonly IViewModelMapper _mapper;

    public AdminApi(IApiHttpHelper http, IOptions<ApiSettings> settings, IViewModelMapper mapper)
    {
        _http = http;
        _settings = settings.Value;
        _mapper = mapper;
    }

    public async Task<List<GroupingVM>> GetGroupingOptions()
    {
        var groupings = await _http.Get<List<GroupingDTO>>(_settings.Endpoints.GroupingOptions);
        return groupings.Select(g => _mapper.Map(g)).ToList();
    }
}
