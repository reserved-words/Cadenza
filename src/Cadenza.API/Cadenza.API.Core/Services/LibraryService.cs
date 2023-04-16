using Cadenza.API.Interfaces;

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

    public async Task<AlbumInfo> Album(int id)
    {
        await PopulateCache();
        return await _cache.Albums.GetAlbum(id);
    }

    public async Task<List<AlbumTrack>> AlbumTracks(int id)
    {
        await PopulateCache();
        return await _cache.Albums.GetAlbumTracks(id);
    }

    public async Task<ArtistInfo> Artist(int id)
    {
        await PopulateCache();
        return await _cache.Artists.GetArtist(id);
    }

    public async Task<List<Album>> ArtistAlbums(int id)
    {
        await PopulateCache();
        return await _cache.Artists.GetAlbums(id);
    }

    public async Task<List<Track>> ArtistTracks(int id)
    {
        await PopulateCache();
        return await _cache.Artists.GetArtistTracks(id);
    }

    public async Task<List<Artist>> Artists()
    {
        await PopulateCache();
        return await _cache.Artists.GetAllArtists();
    }

    public async Task<List<Artist>> GenreArtists(string id)
    {
        await PopulateCache();
        return await _cache.Artists.GetArtistsByGenre(id);
    }

    public async Task<List<Artist>> GetAllArtists()
    {
        await PopulateCache();
        return await _cache.Artists.GetAllArtists();
    }

    public async Task<List<Artist>> GroupingArtists(Grouping id)
    {
        await PopulateCache();
        return await _cache.Artists.GetArtistsByGrouping(id);
    }

    public async Task<List<PlayerItem>> Tag(string id)
    {
        await PopulateCache();
        return await _cache.Tags.GetTag(id);
    }

    public async Task<TrackFull> Track(string id)
    {
        await PopulateCache();
        return await _cache.Tracks.GetTrack(id);
    }

    private async Task PopulateCache()
    {
        await _populater.Populate(true);
    }
}
