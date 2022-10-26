namespace Cadenza.API.Core.Services.Cache;

internal class SearchCache : ISearchCache
{
    private readonly List<SearchableAlbum> _albums = new();
    private readonly List<SearchableArtist> _artists = new();
    private readonly List<SearchableGenre> _genres = new();
    private readonly List<SearchableGrouping> _groupings = new();
    private readonly List<SearchableTag> _tags = new();
    private readonly List<SearchableTrack> _tracks = new();

    public Task Populate(FullLibrary library)
    {
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
        var result = _tags.OfType<PlayerItem>().ToList();
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

    private void PopulateAlbums(FullLibrary library)
    {
        foreach (var a in library.Albums)
        {
            _albums.Add(new SearchableAlbum(a.Id, a.Title, a.ArtistName));
            PopulateTags(a.Tags);
        }
    }

    private void PopulateArtists(FullLibrary library)
    {
        foreach (var a in library.Artists)
        {
            _artists.Add(new SearchableArtist(a.Id, a.Name));
            AddItem(_groupings, a.Grouping.ToString(), () => new SearchableGrouping(a.Grouping));
            AddItem(_genres, a.Genre, () => new SearchableGenre(a.Genre ?? ""));
            PopulateTags(a.Tags);
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

            _tracks.Add(new SearchableTrack(
                t.Id,
                t.Title,
                artist.Name,
                album.Name,
                album.Artist));

            PopulateTags(t.Tags);
        }
    }

    private void PopulateTags(TagList tags)
    {
        foreach (var tag in tags.Tags)
        {
            AddItem(_tags, tag, () => new SearchableTag(tag));
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