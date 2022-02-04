namespace Cadenza.Domain;

public class SearchableItem
{
    public SearchableItem()
    {

    }

    public SearchableItem(SearchableItemType type, string id, string name, string artist, string album, string albumDisplay)
    {
        Type = type;
        Id = id;
        Name = name;
        Artist = artist;
        Album = album;
        AlbumDisplay = albumDisplay;
    }

    public SearchableItemType Type { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string AlbumDisplay { get; set; }
}

public class SearchableAlbum : SearchableItem
{
    public SearchableAlbum(string id, string title, string artist)
        : base(SearchableItemType.Album, id, title, artist, null, null) { }
}

public class SearchableArtist : SearchableItem
{
    public SearchableArtist(string id, string name)
        : base(SearchableItemType.Artist, id, name, null, null, null) { }
}

public class SearchablePlaylist : SearchableItem
{
    public SearchablePlaylist(string id, string title)
        : base(SearchableItemType.Playlist, id, title, null, null, null) { }
}

public class SearchableTrack : SearchableItem
{
    public SearchableTrack(string id, string title, string artist, string album, string albumArtist)
        : base(SearchableItemType.Track, id, title, artist, album, album + (albumArtist == artist ? "" : $" ({albumArtist})")) { }
}

