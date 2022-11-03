namespace Cadenza.API.Cache.Services;

internal class ItemCache : IItemCache
{
    private readonly Dictionary<PlayerItemType, List<PlayerItem>> _items = new();
    private readonly Dictionary<string, List<PlayerItem>> _tags = new();

    public void CacheAllbum(AlbumInfo album)
    {
        var item = new SearchableAlbum(album);
        _items.Cache(PlayerItemType.Album, item);
        _tags.Cache(album.Tags, item);
    }

    public void CacheArtist(ArtistInfo artist)
    {
        var item = new SearchableArtist(artist);
        _items.Cache(PlayerItemType.Artist, item);
        _items.Cache(PlayerItemType.Grouping, artist.Grouping.ToString(), () => new SearchableGrouping(artist.Grouping));
        _items.Cache(PlayerItemType.Genre, artist.Genre, () => new SearchableGenre(artist.Genre));
        _tags.Cache(artist.Tags, item);
    }

    public void CacheTrack(TrackInfo track, AlbumInfo album)
    {
        var item = new SearchableTrack(track, album);
        _items.Cache(PlayerItemType.Track, item);
        _tags.Cache(track.Tags, item);
    }

    public void Clear()
    {
        _items.Clear();
        _tags.Clear();
    }

    public List<PlayerItem> GetTag(string id)
    {
        return _tags[id];
    }

    public List<PlayerItem> GetTags()
    {
        return _tags.Keys
            .Select(t => new SearchableTag(t))
            .OfType<PlayerItem>().ToList();
    }

    public List<PlayerItem> GetItems(PlayerItemType type)
    {
        return _items.GetValue(type);
    }
}
