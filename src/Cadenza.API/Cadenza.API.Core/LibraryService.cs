using Cadenza.Domain.Model.Album;
using Cadenza.Domain.Model.Artist;
using Cadenza.Domain.Model.Track;

namespace Cadenza.API.Core;

internal class LibraryService : ILibraryService
{
    private readonly ILibraryCache _cache;
    private readonly ICachePopulater _populater;

    public LibraryService(ILibraryCache cache, ICachePopulater populater)
    {
        _cache = cache;
        _populater = populater;
    }

    public async Task<AlbumInfo> Album(string id)
    {
        await PopulateCache();
        return await _cache.AlbumCache.GetAlbum(id);
    }

    public async Task<List<Artist>> AlbumArtists()
    {
        await PopulateCache();
        return await _cache.ArtistCache.GetAlbumArtists();
    }

    public async Task<List<AlbumTrack>> AlbumTracks(string id)
    {
        await PopulateCache();
        return await _cache.AlbumCache.GetTracks(id);
    }

    public async Task<ArtistInfo> Artist(string id)
    {
        await PopulateCache();
        return await _cache.ArtistCache.GetArtist(id);
    }

    public async Task<List<Album>> ArtistAlbums(string id)
    {
        await PopulateCache();
        return await _cache.ArtistCache.GetAlbums(id);
    }

    public async Task<List<Artist>> Artists()
    {
        await PopulateCache();
        return await _cache.ArtistCache.GetAllArtists();
    }

    public async Task<List<Artist>> GenreArtists(string id)
    {
        await PopulateCache();
        return await _cache.ArtistCache.GetArtistsByGenre(id);
    }

    public async Task<List<Artist>> GetAllArtists()
    {
        await PopulateCache();
        return await _cache.ArtistCache.GetAllArtists();
    }

    public async Task<List<Artist>> GroupingArtists(Grouping id)
    {
        await PopulateCache();
        return await _cache.ArtistCache.GetArtistsByGrouping(id);
    }

    public async Task<TrackFull> Track(string id)
    {
        await PopulateCache();
        return await _cache.TrackCache.GetTrack(id);
    }

    public async Task<List<Artist>> TrackArtists()
    {
        await PopulateCache();
        return await _cache.ArtistCache.GetTrackArtists();
    }

    private async Task PopulateCache()
    {
        await _populater.Populate(true);
    }
}
