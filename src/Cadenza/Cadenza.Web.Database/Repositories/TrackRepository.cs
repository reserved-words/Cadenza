namespace Cadenza.Web.Database.Repositories;

internal class TrackRepository : ITrackRepository
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;
    private readonly IViewModelMapper _mapper;

    public TrackRepository(IOptions<DatabaseApiSettings> settings, IApiHttpHelper apiHelper, IViewModelMapper mapper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
        _mapper = mapper;
    }

    public async Task<TrackFullVM> GetTrack(int id)
    {
        var track = await _apiHelper.Get<TrackFullDTO>(_settings.Track, id);
        return _mapper.Map(track);
    }

}
