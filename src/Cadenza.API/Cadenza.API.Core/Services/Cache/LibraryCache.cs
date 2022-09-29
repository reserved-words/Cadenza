using Cadenza.Common.Domain.Model;
using Cadenza.Common.Interfaces.Repositories;

namespace Cadenza.API.Core.Services.Cache;

internal class LibraryCache : ILibraryCache
{
    private readonly IAlbumCache _albumCache;
    private readonly IArtistCache _artistCache;
    private readonly IPlayTrackCache _playTrackCache;
    private readonly ISearchCache _searchCache;
    private readonly ITrackCache _trackCache;

    public LibraryCache(IAlbumCache albumCache, IArtistCache artistCache, IPlayTrackCache playTrackCache, ISearchCache searchCache, ITrackCache trackCache)
    {
        _albumCache = albumCache;
        _artistCache = artistCache;
        _playTrackCache = playTrackCache;
        _searchCache = searchCache;
        _trackCache = trackCache;
    }

    public IAlbumRepository AlbumCache => _albumCache;

    public IArtistRepository ArtistCache => _artistCache;

    public IPlayTrackRepository PlayTrackCache => _playTrackCache;

    public ISearchRepository SearchCache => _searchCache;

    public ITrackRepository TrackCache => _trackCache;

    public bool IsPopulated { get; private set; } = false;

    public async Task Populate(FullLibrary library)
    {
        await _albumCache.Populate(library);
        await _artistCache.Populate(library);
        await _playTrackCache.Populate(library);
        await _searchCache.Populate(library);
        await _trackCache.Populate(library);
        IsPopulated = true;
    }
}
