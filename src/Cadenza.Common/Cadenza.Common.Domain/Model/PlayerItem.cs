namespace Cadenza.Common.Domain.Model;

public class PlayerItem
{
    public PlayerItem()
    {

    }

    public PlayerItem(PlayerItemType type, string id, string name, string artist, string album, string albumDisplay)
    {
        Type = type;
        Id = id;
        Name = name;
        Artist = artist;
        Album = album;
        AlbumDisplay = albumDisplay;
    }

    public PlayerItemType Type { get; set; }
    public string Id { get; set; }
    public string Name { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string AlbumDisplay { get; set; }
}

public class SearchableAlbum : PlayerItem
{
    public SearchableAlbum(string id, string title, string artist)
        : base(PlayerItemType.Album, id, title, artist, null, null) { }

    public SearchableAlbum(AlbumInfo album)
        : this(album.Id, album.Title, album.ArtistName) { }
}

public class SearchableArtist : PlayerItem
{
    public SearchableArtist(string id, string name)
        : base(PlayerItemType.Artist, id, name, null, null, null) { }

    public SearchableArtist(ArtistInfo artist)
        : this(artist.Id, artist.Name) { }
}

public class SearchableTag : PlayerItem
{
    public SearchableTag(string id)
        : base(PlayerItemType.Tag, id, id, null, null, null) { }
}

public class SearchableTrack : PlayerItem
{
    public SearchableTrack(string id, string title, string artist, string album, string albumArtist)
        : base(PlayerItemType.Track, id, title, artist, album, album + (albumArtist == artist ? "" : $" ({albumArtist})")) { }

    public SearchableTrack(TrackInfo track, AlbumInfo album)
        : this(track.Id, track.Title, track.ArtistName, album.Title, album.ArtistName) { }
}

public class SearchableGrouping : PlayerItem
{
    public SearchableGrouping(Grouping id)
        : base(PlayerItemType.Grouping, id.ToString(), id.GetDisplayName(), null, null, null) { }
}

public class SearchableGenre : PlayerItem
{
    public SearchableGenre(string id)
        : base(PlayerItemType.Genre, id, id, null, null, null) { }
}

