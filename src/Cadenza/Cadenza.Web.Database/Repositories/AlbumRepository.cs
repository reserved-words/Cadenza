namespace Cadenza.Web.Database.Repositories;

internal class AlbumRepository : IAlbumRepository
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;
    private readonly IViewModelMapper _mapper;

    public AlbumRepository(IOptions<DatabaseApiSettings> settings, IApiHttpHelper apiHelper, IViewModelMapper mapper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
        _mapper = mapper;
    }

    public async Task<AlbumDetailsVM> GetAlbum(int id)
    {
        var album = await _apiHelper.Get<AlbumDetailsDTO>(_settings.Album, id);
        return _mapper.Map(album);
    }
    public async Task<List<AlbumTrackVM>> GetAlbumTracks(int id)
    {
        var tracks = await _apiHelper.Get<List<AlbumTrackDTO>>(_settings.AlbumTracks, id);
        return tracks.Select(t => _mapper.Map(t)).ToList();
    }
}
