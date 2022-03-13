using Cadenza.Library;
using Cadenza.Local.API.Interfaces;

namespace Cadenza.Local.API;

public class ApiLibraryService : IApiLibraryService
{
    private readonly ILibrary _library;
    private readonly IAlbumCache _albumCache;
    private readonly IArtistCache _artistCache;
    private readonly IPlayTrackCache _playTrackCache;
    private readonly ISearchCache _searchCache;
    private readonly ITrackCache _trackCache;

    public ApiLibraryService(ILibrary library, IAlbumCache albumCache, IArtistCache artistCache, IPlayTrackCache playTrackCache, ISearchCache searchCache, ITrackCache trackCache)
    {
        _library = library;
        _albumCache = albumCache;
        _artistCache = artistCache;
        _playTrackCache = playTrackCache;
        _searchCache = searchCache;
        _trackCache = trackCache;
    }

    public async Task<AlbumInfo> GetAlbum(string id)
    {
        return await _albumCache.GetAlbum(id);
    }

    public async Task<ListResponse<Artist>> GetAlbumArtists(int page, int limit)
    {
        return await _artistCache.GetAlbumArtists(page, limit);
    }

    public async Task<ListResponse<Album>> GetAlbums(string id, int page, int limit)
    {
        return await _artistCache.GetAlbums(id, page, limit);
    }

    public async Task<ListResponse<Artist>> GetAllArtists(int page, int limit)
    {
        return await _artistCache.GetAllArtists(page, limit);
    }

    public async Task<ArtistInfo> GetArtist(string id)
    {
        return await _artistCache.GetArtist(id);
    }

    public async Task<ListResponse<Artist>> GetArtistsByGenre(string id, int page, int limit)
    {
        return await _artistCache.GetArtistsByGenre(id, page, limit);
    }

    public async Task<ListResponse<Artist>> GetArtistsByGrouping(Grouping id, int page, int limit)
    {
        return await _artistCache.GetArtistsByGrouping(id, page, limit);
    }

    public async Task<TrackFull> GetTrack(string id)
    {
        return await _trackCache.GetTrack(id);
    }

    public async Task<ListResponse<Artist>> GetTrackArtists(int page, int limit)
    {
        return await _artistCache.GetTrackArtists(page, limit);
    }

    public async Task<List<AlbumTrack>> GetTracks(string id)
    {
        return await _albumCache.GetTracks(id);
    }

    public async Task Populate()
    {
        await _library.Populate();
        var fullLibrary = await _library.Get();
        await _albumCache.Populate(fullLibrary);
        await _artistCache.Populate(fullLibrary);
        await _playTrackCache.Populate(fullLibrary);
        await _searchCache.Populate(fullLibrary);
        await _trackCache.Populate(fullLibrary);
    }
}