namespace Cadenza.Web.Database.Repositories;

internal class AlbumRepository : IAlbumRepository
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;

    public AlbumRepository(IOptions<DatabaseApiSettings> settings, IApiHttpHelper apiHelper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
    }

    public async Task<AlbumDetails> GetAlbum(int id)
    {
        return await _apiHelper.Get<AlbumDetails>(_settings.Album, id);
    }
    public async Task<List<AlbumTrack>> GetAlbumTracks(int id)
    {
        return await _apiHelper.Get<List<AlbumTrack>>(_settings.AlbumTracks, id);
    }
}
