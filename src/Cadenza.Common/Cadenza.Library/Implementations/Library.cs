namespace Cadenza.Library;

public class Library : ILibrary
{
    private readonly IStaticSource _overrides;
    private readonly IStaticSource _source;
    private readonly IStaticLibraryCacher _combiner;

    private Cache _combinedLibrary;

    internal Library(IStaticLibraryCacher combiner, IStaticSource source, IStaticSource overrides = null)
    {
        _combiner = combiner;
        _overrides = overrides;
        _source = source;
    }

    public async Task<PlayingTrack> GetTrack(string id)
    {
        await PopulateStaticSources();
        return await _combinedLibrary.GetTrack(id);
    }

    public async Task<FullTrack> GetFullTrack(string id)
    {
        await PopulateStaticSources();
        return await _combinedLibrary.GetFullTrack(id);
    }

    public async Task<IEnumerable<ArtistInfo>> GetArtists()
    {
        await PopulateStaticSources();
        return await _combinedLibrary.GetArtists();
    }

    public async Task<IEnumerable<AlbumInfo>> GetAlbums()
    {
        await PopulateStaticSources();
        return await _combinedLibrary.GetAlbums();
    }

    public async Task<IEnumerable<string>> GetAlbumTracks(string artistId, string albumId)
    {
        await PopulateStaticSources();
        return await _combinedLibrary.GetAlbumTracks(artistId, albumId);
    }

    public async Task<IEnumerable<string>> GetArtistTracks(string id)
    {
        await PopulateStaticSources();
        return await _combinedLibrary.GetArtistTracks(id);
    }

    public async Task<IEnumerable<string>> GetAllTracks()
    {
        await PopulateStaticSources();
        return await _combinedLibrary.GetAllTracks();
    }

    private async Task PopulateStaticSources()
    {
        if (_combinedLibrary != null)
            return;

        var getSource = _source.GetStaticLibrary();
        var getOverrides = _overrides == null 
            ? Task.FromResult((StaticLibrary)null) 
            : _overrides.GetStaticLibrary();

        await Task.WhenAll(getSource, getOverrides)
            .ContinueWith(t =>
            {
                var library = getSource.Result;
                if (getOverrides.Result != null)
                {
                    _combiner.MergeStaticLibrary(library, getOverrides.Result, MergeMode.ReplaceIfUpdateIsNotEmpty);
                }
                _combinedLibrary = new Cache(library);
            });
    }
}