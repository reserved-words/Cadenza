namespace Cadenza.Web.Api.Services;

internal class AlbumApi : IAlbumApi
{
    private readonly ApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;
    private readonly IViewModelMapper _mapper;

    public AlbumApi(IOptions<ApiSettings> settings, IApiHttpHelper apiHelper, IViewModelMapper mapper)
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

    public async Task<AlbumTracksVM> GetAlbumTracks(int id)
    {
        var dto = await _apiHelper.Get<AlbumTracksDTO>(_settings.AlbumTracks, id);
        return _mapper.Map(dto);
    }
}
