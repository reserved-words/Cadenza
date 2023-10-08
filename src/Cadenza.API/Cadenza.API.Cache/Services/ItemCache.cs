namespace Cadenza.API.Cache.Services;

internal class ItemCache : IItemCache
{
    private readonly Dictionary<PlayerItemType, List<PlayerItemDTO>> _items = new();
    private readonly Dictionary<string, List<PlayerItemDTO>> _tags = new();

    public void CacheAlbum(AlbumDetailsDTO album)
    {
        var item = new SearchableAlbum(album);
        _items.Cache(PlayerItemType.Album, item);
        _tags.Cache(album.Tags.Tags, item);
    }

    public void CacheArtist(ArtistDetailsDTO artist)
    {
        var item = new SearchableArtist(artist);
        _items.Cache(PlayerItemType.Artist, item);
        _items.Cache(PlayerItemType.Grouping, artist.Grouping.Id.ToString(), () => new SearchableGrouping(artist.Grouping));
        _items.Cache(PlayerItemType.Genre, artist.Genre, () => new SearchableGenre(artist.Genre));
        _tags.Cache(artist.Tags.Tags, item);
    }

    public void CacheTrack(TrackDetailsDTO track, AlbumDetailsDTO album)
    {
        var item = new SearchableTrack(track, album);
        _items.Cache(PlayerItemType.Track, item);
        _tags.Cache(track.Tags.Tags, item);
    }

    public void Clear()
    {
        _items.Clear();
        _tags.Clear();
    }

    public List<PlayerItemDTO> GetTag(string id)
    {
        return _tags[id];
    }

    public List<PlayerItemDTO> GetTags()
    {
        return _tags.Keys
            .Select(t => new SearchableTag(t))
            .OfType<PlayerItemDTO>().ToList();
    }

    public List<PlayerItemDTO> GetItems(PlayerItemType type)
    {
        return _items.GetValue(type);
    }
}
