namespace Cadenza.Web.Database.Repositories;

internal class TagRepository : ITagRepository
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;
    private readonly IViewModelMapper _mapper;

    public TagRepository(IOptions<DatabaseApiSettings> settings, IApiHttpHelper apiHelper, IViewModelMapper mapper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
        _mapper = mapper;
    }

    public async Task<List<TaggedItemVM>> GetTag(string id)
    {
        var items = await _apiHelper.Get<List<TaggedItemDTO>>(_settings.Tag, id);
        return items.Select(i => _mapper.Map(i)).ToList();
    }

}
