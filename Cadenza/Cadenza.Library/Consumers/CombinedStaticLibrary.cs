namespace Cadenza.Library;

public class CombinedStaticLibrary : ILibrary
{
    private readonly List<StaticSourceManager> _sources;

    private readonly ILibrary _cache;
    private readonly IStaticLibraryCacher _libraryCacher;

    public CombinedStaticLibrary(ICache cache, IMerger merger, List<IStaticSource> staticSources)
    {
        var itemCacher = new SimpleCacher(merger, cache);

        _cache = new CachedLibrary(cache);
        _libraryCacher = new StaticLibraryCacher(itemCacher);
        _sources = staticSources.Select(s => new StaticSourceManager(s)).ToList();
    }

    public async Task<ArtistFull> GetAlbumArtist(string id)
    {
        await PopulateStaticSources();
        return await _cache.GetAlbumArtist(id);
    }

    public async Task<ICollection<Artist>> GetAlbumArtists()
    {
        await PopulateStaticSources();
        return await _cache.GetAlbumArtists();
    }

    public async Task<ICollection<Track>> GetAllTracks()
    {
        await PopulateStaticSources();
        return await _cache.GetAllTracks();
    }

    public async Task<PlayingTrack> GetTrack(string id)
    {
        await PopulateStaticSources();
        return await _cache.GetTrack(id);
    }

    public async Task<FullTrack> GetFullTrack(string id)
    {
        await PopulateStaticSources();
        return await _cache.GetFullTrack(id);
    }

    private async Task PopulateStaticSources()
    {
        foreach (var source in _sources)
        {
            var library = await source.Fetch();
            _libraryCacher.AddStaticLibrary(library, false);
        }
    }
}