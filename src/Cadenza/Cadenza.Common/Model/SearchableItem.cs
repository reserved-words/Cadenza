namespace Cadenza.Common;

public abstract class SearchableItem
{
    public SearchableItem(SearchableItemType type, string id, string name, string artist = null, string album = null)
    {
        Type = type;
        Id = id;
        Name = name;
        Artist = artist;
        Album = album;
    }

    public SearchableItemType Type { get; }
    public string Id { get; }
    public string Name { get; }
    public string Artist { get; }
    public string Album { get; }
}

public class SearchableArtist : SearchableItem
{
    public SearchableArtist(string id, string name)
        : base(SearchableItemType.Artist, id, name, null, null) { }
}

public class SearchableAlbum : SearchableItem
{
    public SearchableAlbum(string id, string title, string artist)
        : base(SearchableItemType.Album, id, title, artist, null) { }
}

public class SearchableTrack : SearchableItem
{
    public SearchableTrack(string id, string title, string artist, string album, string albumArtist)
        : base(SearchableItemType.Track, id, title, artist, album + (albumArtist == artist ? "" : $" ({albumArtist})")) { }
}

