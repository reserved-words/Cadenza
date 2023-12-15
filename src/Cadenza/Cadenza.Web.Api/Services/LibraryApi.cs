namespace Cadenza.Web.Api.Services;

internal class LibraryApi : ILibraryApi
{
    private readonly ApiEndpoints _settings;
    private readonly IApiHttpHelper _apiHelper;
    private readonly IViewModelMapper _mapper;

    public LibraryApi(IOptions<ApiSettings> settings, IApiHttpHelper apiHelper, IViewModelMapper mapper)
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

    public async Task<List<AlbumVM>> GetAlbums(int id)
    {
        var albums = await _apiHelper.Get<List<AlbumDTO>>(_settings.ArtistAlbums, id);
        return albums.Select(a => _mapper.Map(a)).ToList();
    }

    public async Task<List<AlbumVM>> GetAlbumsFeaturingArtist(int artistId)
    {
        var albums = await _apiHelper.Get<List<AlbumDTO>>(_settings.AlbumsFeaturingArtist, artistId);
        return albums.Select(a => _mapper.Map(a)).ToList();
    }

    public async Task<List<AlbumDiscVM>> GetAlbumTracks(int id)
    {
        var discs = await _apiHelper.Get<List<AlbumDiscDTO>>(_settings.AlbumTracks, id);
        return discs.Select(d => _mapper.Map(d)).ToList();
    }

    public async Task<ArtistDetailsVM> GetArtist(int id)
    {
        var artist = await _apiHelper.Get<ArtistDetailsDTO>(_settings.Artist, id);
        return _mapper.Map(artist);
    }

    public async Task<List<ArtistVM>> GetArtistsByGenre(string id)
    {
        var artists = await _apiHelper.Get<List<ArtistDTO>>(_settings.GenreArtists, id);
        return artists.Select(a => _mapper.Map(a)).ToList();
    }

    public async Task<List<ArtistVM>> GetArtistsByGrouping(int id)
    {
        var artists = await _apiHelper.Get<List<ArtistDTO>>(_settings.GroupingArtists, id);
        return artists.Select(a => _mapper.Map(a)).ToList();
    }

    public async Task<List<TaggedItemVM>> GetTag(string id)
    {
        var items = await _apiHelper.Get<List<TaggedItemDTO>>(_settings.Tag, id);
        return items.Select(i => _mapper.Map(i)).ToList();
    }

    public async Task<TrackFullVM> GetTrack(int id)
    {
        var track = await _apiHelper.Get<TrackFullDTO>(_settings.Track, id);
        return _mapper.Map(track);
    }
}
