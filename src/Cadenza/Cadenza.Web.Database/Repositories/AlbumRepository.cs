namespace Cadenza.Web.Database.Repositories;

internal class AlbumRepository : IAlbumRepository
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHelper _apiHelper;

    public AlbumRepository(IOptions<DatabaseApiSettings> settings, IApiHelper apiHelper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
    }

    public async Task<AlbumInfo> GetAlbum(int id)
    {
        return await _apiHelper.Get<AlbumInfo>(_settings.Album, id);
    }
    public async Task<List<AlbumTrack>> GetAlbumTracks(int id)
    {
        return await _apiHelper.Get<List<AlbumTrack>>(_settings.AlbumTracks, id);
    }
}
