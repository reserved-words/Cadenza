using Cadenza.Domain.Enums;
using Cadenza.Domain.Extensions;

namespace Cadenza.Domain.Models;

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
}

public class SearchableArtist : PlayerItem
{
    public SearchableArtist(string id, string name)
        : base(PlayerItemType.Artist, id, name, null, null, null) { }
}

public class SearchablePlaylist : PlayerItem
{
    public SearchablePlaylist(string id, string title)
        : base(PlayerItemType.Playlist, id, title, null, null, null) { }
}

public class SearchableTrack : PlayerItem
{
    public SearchableTrack(string id, string title, string artist, string album, string albumArtist)
        : base(PlayerItemType.Track, id, title, artist, album, album + (albumArtist == artist ? "" : $" ({albumArtist})")) { }
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

