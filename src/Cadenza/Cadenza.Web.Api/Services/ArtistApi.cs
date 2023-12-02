using Cadenza.Web.Api.Interfaces;
using Cadenza.Web.Api.Settings;

namespace Cadenza.Web.Api.Services;

internal class ArtistApi : IArtistApi
{
    private readonly ApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;
    private readonly IViewModelMapper _mapper;

    public ArtistApi(IOptions<ApiSettings> settings, IApiHttpHelper apiHelper, IViewModelMapper mapper)
    {
        _settings = settings.Value.Endpoints;
        _apiHelper = apiHelper;
        _mapper = mapper;
    }

    public async Task<List<AlbumVM>> GetAlbumsFeaturingArtist(int artistId)
    {
        var albums = await _apiHelper.Get<List<AlbumDTO>>(_settings.AlbumsFeaturingArtist, artistId);
        return albums.Select(a => _mapper.Map(a)).ToList();
    }

    public async Task<List<ArtistVM>> GetArtistsByGrouping(int id)
    {
        var artists = await _apiHelper.Get<List<ArtistDTO>>(_settings.GroupingArtists, id);
        return artists.Select(a => _mapper.Map(a)).ToList();
    }

    public async Task<List<ArtistVM>> GetArtistsByGenre(string id)
    {
        var artists = await _apiHelper.Get<List<ArtistDTO>>(_settings.GenreArtists, id);
        return artists.Select(a => _mapper.Map(a)).ToList();
    }

    public async Task<ArtistDetailsVM> GetArtist(int id)
    {
        var artist = await _apiHelper.Get<ArtistDetailsDTO>(_settings.Artist, id);
        return _mapper.Map(artist);
    }

    public async Task<List<AlbumVM>> GetAlbums(int id)
    {
        var albums = await _apiHelper.Get<List<AlbumDTO>>(_settings.ArtistAlbums, id);
        return albums.Select(a => _mapper.Map(a)).ToList();
    }
}
