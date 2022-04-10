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

    public async Task<List<Artist>> GetAlbumArtists()
    {
        return await _artistCache.GetAlbumArtists();
    }

    public async Task<List<Album>> GetAlbums(string id)
    {
        return await _artistCache.GetAlbums(id);
    }

    public async Task<List<Artist>> GetAllArtists()
    {
        return await _artistCache.GetAllArtists();
    }

    public async Task<ArtistInfo> GetArtist(string id)
    {
        return await _artistCache.GetArtist(id);
    }

    public async Task<List<Artist>> GetArtistsByGenre(string id)
    {
        return await _artistCache.GetArtistsByGenre(id);
    }

    public async Task<List<Artist>> GetArtistsByGrouping(Grouping id)
    {
        return await _artistCache.GetArtistsByGrouping(id);
    }

    public async Task<TrackFull> GetTrack(string id)
    {
        return await _trackCache.GetTrack(id);
    }

    public async Task<List<Artist>> GetTrackArtists()
    {
        return await _artistCache.GetTrackArtists();
    }

    public async Task<List<AlbumTrack>> GetTracks(string id)
    {
        return await _albumCache.GetTracks(id);
    }

    public async Task Populate()
    {
        var fullLibrary = await _library.Get();
        await _albumCache.Populate(fullLibrary);
        await _artistCache.Populate(fullLibrary);
        await _playTrackCache.Populate(fullLibrary);
        await _searchCache.Populate(fullLibrary);
        await _trackCache.Populate(fullLibrary);
    }
}