namespace Cadenza.Library;

public class CombinedLibrary : ILibrary
{
    private readonly IEnumerable<IStaticSource> _sources;

    private readonly ILibrary _cache;
    private readonly IStaticLibraryCacher _libraryCacher;

    internal CombinedLibrary(IStaticLibraryCacher cacher, ILibrary cache, IEnumerable<IStaticSource> sources)
    {
        _cache = cache;
        _libraryCacher = cacher;
        _sources = sources;
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
            var library = await Fetch(source);
            _libraryCacher.AddStaticLibrary(library, false);
        }
    }

    private async Task<StaticLibrary> Fetch(IStaticSource source)
    {
        return await source.GetStaticLibrary();
    }
}