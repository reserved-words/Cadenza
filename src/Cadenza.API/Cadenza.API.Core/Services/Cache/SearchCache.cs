namespace Cadenza.API.Core.Services.Cache;

internal class SearchCache : ISearchCache
{
    private readonly List<SearchableAlbum> _albums = new();
    private readonly List<SearchableArtist> _artists = new();
    private readonly List<SearchableGenre> _genres = new();
    private readonly List<SearchableGrouping> _groupings = new();
    private readonly List<SearchableTrack> _tracks = new();

    private readonly Dictionary<string, List<PlayerItem>> _tags = new();

    public Task Populate(FullLibrary library)
    {
        _albums.Clear();
        _artists.Clear();
        _genres.Clear();
        _groupings.Clear();
        _tracks.Clear();
        _tags.Clear();

        PopulateAlbums(library);

        PopulateArtists(library);

        PopulateSearchableTracks(library);

        return Task.CompletedTask;
    }

    public Task<List<PlayerItem>> GetSearchAlbums()
    {
        var result = _albums.OfType<PlayerItem>().ToList();
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetSearchArtists()
    {
        var result = _artists.OfType<PlayerItem>().ToList();
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetSearchTags()
    {
        var result = _tags.Keys.Select(t => new SearchableTag(t)).OfType<PlayerItem>().ToList();
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetSearchTracks()
    {
        var result = _tracks.OfType<PlayerItem>().ToList();
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetSearchGenres()
    {
        var result = _genres.OfType<PlayerItem>().ToList();
        return Task.FromResult(result);
    }

    public Task<List<PlayerItem>> GetSearchGroupings()
    {
        var result = _groupings.OfType<PlayerItem>().ToList();
        return Task.FromResult(result);
    }

    public async Task<List<PlayerItem>> GetTag(string id)
    {
        return _tags[id];
    }

    private void PopulateAlbums(FullLibrary library)
    {
        foreach (var a in library.Albums)
        {
            var item = new SearchableAlbum(a.Id, a.Title, a.ArtistName);
            _albums.Add(item);
            PopulateTags(a.Tags, item);
        }
    }

    private void PopulateArtists(FullLibrary library)
    {
        foreach (var a in library.Artists)
        {
            var item = new SearchableArtist(a.Id, a.Name);
            _artists.Add(item);
            AddItem(_groupings, a.Grouping.ToString(), () => new SearchableGrouping(a.Grouping));
            AddItem(_genres, a.Genre, () => new SearchableGenre(a.Genre ?? ""));
            PopulateTags(a.Tags, item);
        }
    }

    private void PopulateSearchableTracks(FullLibrary library)
    {
        var artistsDict = _artists.ToDictionary(a => a.Id, a => a);
        var albumsDict = _albums.ToDictionary(a => a.Id, a => a);

        foreach (var t in library.Tracks)
        {
            var artist = t.ArtistId == null
                    ? new SearchableArtist("", "No Artist Found")
                    : artistsDict[t.ArtistId];

            var album = t.AlbumId == null
                ? new SearchableAlbum("", "No Album Found", artist.Name)
                : albumsDict[t.AlbumId];

            var item = new SearchableTrack(
                t.Id,
                t.Title,
                artist.Name,
                album.Name,
                album.Artist);

            _tracks.Add(item);

            PopulateTags(t.Tags, item);
        }
    }

    private void PopulateTags(TagList tags, PlayerItem item)
    {
        foreach (var tag in tags.Tags)
        {
            if (!_tags.TryGetValue(tag, out List<PlayerItem> list))
            {
                list = new List<PlayerItem>();
                _tags.Add(tag, list);
            }

            list.Add(item);
        }
    }

    private void AddItem<T>(List<T> items, string id, Func<T> create) where T : PlayerItem
    {
        if (!items.Any(t => t.Id == id))
        {
            items.Add(create());
        }
    }
}