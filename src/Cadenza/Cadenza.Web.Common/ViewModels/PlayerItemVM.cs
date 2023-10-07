namespace Cadenza.Web.Common.ViewModels;

public class PlayerItemVM
{
    public PlayerItemVM()
    {

    }

    public PlayerItemVM(PlayerItemType type, string id, string name, string artist, string album, string albumDisplay)
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

public class SearchableAlbum : PlayerItemVM
{
    public SearchableAlbum(string id, string title, string artist)
        : base(PlayerItemType.Album, id, title, artist, null, null) { }

    public SearchableAlbum(AlbumDetailsVM album)
        : this(album.Id.ToString(), album.Title, album.ArtistName) { }
}

public class SearchableArtist : PlayerItemVM
{
    public SearchableArtist(string id, string name)
        : base(PlayerItemType.Artist, id, name, null, null, null) { }

    public SearchableArtist(ArtistDetailsVM artist)
        : this(artist.Id.ToString(), artist.Name) { }
}

public class SearchableTag : PlayerItemVM
{
    public SearchableTag(string id)
        : base(PlayerItemType.Tag, id, id, null, null, null) { }
}

public class SearchableTrack : PlayerItemVM
{
    public SearchableTrack(string id, string title, string artist, string album, string albumArtist)
        : base(PlayerItemType.Track, id, title, artist, album, album + (albumArtist == artist ? "" : $" ({albumArtist})")) { }

    public SearchableTrack(TrackDetailsVM track, AlbumDetailsVM album)
        : this(track.Id.ToString(), track.Title, track.ArtistName, album.Title, album.ArtistName) { }
}

public class SearchableGrouping : PlayerItemVM
{
    public SearchableGrouping(GroupingVM grouping)
        : base(PlayerItemType.Grouping, grouping.Id.ToString(), grouping.Name, null, null, null) { }
}

public class SearchableGenre : PlayerItemVM
{
    public SearchableGenre(string id)
        : base(PlayerItemType.Genre, id, id, null, null, null) { }
}

