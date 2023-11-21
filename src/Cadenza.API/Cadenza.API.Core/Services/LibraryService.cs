namespace Cadenza.API.Core.Services;

internal class LibraryService : ILibraryService
{
    private readonly ILibraryCache _cache;
    private readonly ICachePopulater _populater;

    public LibraryService(ILibraryCache cache, ICachePopulater populater)
    {
        _cache = cache;
        _populater = populater;
    }

    public async Task<AlbumDetailsDTO> Album(int id)
    {
        await PopulateCache();
        return await _cache.Albums.GetAlbum(id);
    }

    public async Task<List<AlbumDTO>> AlbumsFeaturingArtist(int id)
    {
        await PopulateCache();
        return await _cache.Artists.GetAlbumsFeaturingArtist(id);
    }

    public async Task<AlbumTracksDTO> AlbumTracks(int id)
    {
        await PopulateCache();
        return await _cache.Albums.GetAlbumTracks(id);
    }

    public async Task<ArtistDetailsDTO> Artist(int id)
    {
        await PopulateCache();
        return await _cache.Artists.GetArtist(id);
    }

    public async Task<List<AlbumDTO>> ArtistAlbums(int id)
    {
        await PopulateCache();
        return await _cache.Artists.GetAlbums(id);
    }

    public async Task<List<TrackDTO>> ArtistTracks(int id)
    {
        await PopulateCache();
        return await _cache.Artists.GetArtistTracks(id);
    }

    public async Task<List<ArtistDTO>> Artists()
    {
        await PopulateCache();
        return await _cache.Artists.GetAllArtists();
    }

    public async Task<List<ArtistDTO>> GenreArtists(string id)
    {
        await PopulateCache();
        return await _cache.Artists.GetArtistsByGenre(id);
    }

    public async Task<List<ArtistDTO>> GetAllArtists()
    {
        await PopulateCache();
        return await _cache.Artists.GetAllArtists();
    }

    public async Task<List<ArtistDTO>> GroupingArtists(int id)
    {
        await PopulateCache();
        return await _cache.Artists.GetArtistsByGrouping(id);
    }

    public async Task<List<PlayerItemDTO>> Tag(string id)
    {
        await PopulateCache();
        return await _cache.Tags.GetTag(id);
    }

    public async Task<TrackFullDTO> Track(int id)
    {
        await PopulateCache();
        return await _cache.Tracks.GetTrack(id);
    }

    private async Task PopulateCache()
    {
        await _populater.Populate(true);
    }
}
