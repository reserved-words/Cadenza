namespace Cadenza.Web.Database.Repositories;

internal class AdminRepository : IAdminRepository
{
    private readonly DatabaseApiSettings _settings;
    private readonly IApiHttpHelper _http;
    private readonly IViewModelMapper _mapper;

    public AdminRepository(IApiHttpHelper http, IOptions<DatabaseApiSettings> settings, IViewModelMapper mapper)
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
