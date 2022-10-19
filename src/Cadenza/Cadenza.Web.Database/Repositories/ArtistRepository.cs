namespace Cadenza.Web.Database.Repositories;

internal class ArtistRepository : IArtistRepository
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHelper _apiHelper;

    public ArtistRepository(IOptions<DatabaseApiSettings> settings, IApiHelper apiHelper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
    }

    public async Task<List<Artist>> GetAllArtists()
    {
        return await _apiHelper.Get<List<Artist>>(_settings.AllArtists);
    }

    public async Task<List<Artist>> GetArtistsByGrouping(Grouping id)
    {
        return await _apiHelper.Get<List<Artist>>(_settings.GroupingArtists, id.ToString());
    }

    public async Task<List<Artist>> GetArtistsByGenre(string id)
    {
        return await _apiHelper.Get<List<Artist>>(_settings.GenreArtists, id);
    }

    public async Task<ArtistInfo> GetArtist(string id)
    {
        return await _apiHelper.Get<ArtistInfo>(_settings.Artist, id);
    }

    public async Task<List<Artist>> GetTrackArtists()
    {
        return await _apiHelper.Get<List<Artist>>(_settings.TrackArtists);
    }
    public async Task<List<Artist>> GetAlbumArtists()
    {
        return await _apiHelper.Get<List<Artist>>(_settings.AlbumArtists);
    }

    public async Task<List<Album>> GetAlbums(string id)
    {
        return await _apiHelper.Get<List<Album>>(_settings.ArtistAlbums, id);
    }

    public async Task<List<Track>> GetTracks(string id)
    {
        return await _apiHelper.Get<List<Track>>(_settings.ArtistTracks, id);
    }
}
