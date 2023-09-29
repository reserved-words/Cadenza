using Cadenza.Common.Domain.Model.Library;

namespace Cadenza.Web.Database.Repositories;

internal class ArtistRepository : IArtistRepository
{
    private readonly DatabaseApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;

    public ArtistRepository(IOptions<DatabaseApiSettings> settings, IApiHttpHelper apiHelper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
    }

    public async Task<List<Album>> GetAlbumsFeaturingArtist(int artistId)
    {
        return await _apiHelper.Get<List<Album>>(_settings.AlbumsFeaturingArtist, artistId);
    }

    public async Task<List<Artist>> GetAllArtists()
    {
        return await _apiHelper.Get<List<Artist>>(_settings.AllArtists);
    }

    public async Task<List<Artist>> GetArtistsByGrouping(int id)
    {
        return await _apiHelper.Get<List<Artist>>(_settings.GroupingArtists, id);
    }

    public async Task<List<Artist>> GetArtistsByGenre(string id)
    {
        return await _apiHelper.Get<List<Artist>>(_settings.GenreArtists, id);
    }

    public async Task<ArtistDetails> GetArtist(int id)
    {
        return await _apiHelper.Get<ArtistDetails>(_settings.Artist, id);
    }

    public async Task<List<Album>> GetAlbums(int id)
    {
        return await _apiHelper.Get<List<Album>>(_settings.ArtistAlbums, id);
    }

    public async Task<List<Track>> GetArtistTracks(int id)
    {
        return await _apiHelper.Get<List<Track>>(_settings.ArtistTracks, id);
    }
}
