namespace Cadenza.Library;

public class CombinedSourceLibrary : ICombinedSourceLibrary
{
    private readonly Dictionary<Source, DynamicSourceManager> _sources;

    private readonly ILibrary _cache;
    private readonly ISimpleCacher _itemCacher;
    private readonly IComplexCacher _syncer;

    public CombinedSourceLibrary(ICache cache, IMerger merger, List<SourceLibrary> dynamicSources)
    {
        _cache = new CachedLibrary(cache);
        _itemCacher = new SimpleCacher(merger, cache);
        _syncer = new CacheSyncer(_itemCacher);

        _sources = dynamicSources.ToDictionary(s => s.Source, s => new DynamicSourceManager(s));
    }

    public async Task<ArtistFull> GetAlbumArtist(string id, IEnumerable<Source> sources)
    {
        foreach (var source in sources)
        {
            var service = _sources[source];
            var sourceArtist = await service.GetAlbumArtist(id);
            if (sourceArtist == null)
                continue;

            _syncer.AddArtist(sourceArtist);
        }

        var artist = await _cache.GetAlbumArtist(id);

        artist.Albums = artist.Albums
            .Where(a => sources.Contains(a.Album.Source))
            .ToList();

        return artist;
    }

    public async Task<ICollection<Artist>> GetAlbumArtists(IEnumerable<Source> sources)
    {
        foreach (var source in sources)
        {
            var service = _sources[source];
            var sourceArtists = await service.GetAlbumArtists();
            if (sourceArtists == null)
                continue;

            _syncer.AddAlbumArtists(sourceArtists);
        }

        var artists = await _cache.GetAlbumArtists();

        return artists.Where(a => a.IsInAnySource(sources)).ToList();
    }

    public async Task<ICollection<Track>> GetAllTracks(IEnumerable<Source> sources)
    {
        foreach (var source in sources)
        {
            var service = _sources[source];
            var tracks = await service.GetAllTracks();
            if (tracks == null)
                continue;

            _syncer.AddTracks(tracks);
        }

        return await _cache.GetAllTracks();
    }

    public async Task<TrackFull> GetTrack(string id, Source source)
    {
        await UpdateTrack(id, source);
        return await _cache.GetTrack(id);
    }

    public async Task<TrackSummary> GetTrackSummary(string id, Source source)
    {
        await UpdateTrack(id, source);
        return await _cache.GetTrackSummary(id);
    }

    private async Task UpdateTrack(string id, Source source)
    {
        var service = _sources[source];
        var track = await service.GetTrack(id);
        if (track == null)
            return;

        _syncer.AddTrack(track);
    }
}