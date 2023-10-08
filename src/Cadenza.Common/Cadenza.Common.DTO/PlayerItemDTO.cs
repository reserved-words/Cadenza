namespace Cadenza.Common.DTO;

public class PlayerItemDTO
{
    public PlayerItemDTO()
    {

    }

    public PlayerItemDTO(PlayerItemType type, string id, string name, string artist, string album, string albumDisplay)
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

public class SearchableAlbum : PlayerItemDTO
{
    public SearchableAlbum(string id, string title, string artist)
        : base(PlayerItemType.Album, id, title, artist, null, null) { }

    public SearchableAlbum(AlbumDetailsDTO album)
        : this(album.Id.ToString(), album.Title, album.ArtistName) { }
}

public class SearchableArtist : PlayerItemDTO
{
    public SearchableArtist(string id, string name)
        : base(PlayerItemType.Artist, id, name, null, null, null) { }

    public SearchableArtist(ArtistDetailsDTO artist)
        : this(artist.Id.ToString(), artist.Name) { }
}

public class SearchableTag : PlayerItemDTO
{
    public SearchableTag(string id)
        : base(PlayerItemType.Tag, id, id, null, null, null) { }
}

public class SearchableTrack : PlayerItemDTO
{
    public SearchableTrack(string id, string title, string artist, string album, string albumArtist)
        : base(PlayerItemType.Track, id, title, artist, album, album + (albumArtist == artist ? "" : $" ({albumArtist})")) { }

    public SearchableTrack(TrackDetailsDTO track, AlbumDetailsDTO album)
        : this(track.Id.ToString(), track.Title, track.ArtistName, album.Title, album.ArtistName) { }
}

public class SearchableGrouping : PlayerItemDTO
{
    public SearchableGrouping(GroupingDTO grouping)
        : base(PlayerItemType.Grouping, grouping.Id.ToString(), grouping.Name, null, null, null) { }
}

public class SearchableGenre : PlayerItemDTO
{
    public SearchableGenre(string id)
        : base(PlayerItemType.Genre, id, id, null, null, null) { }
}

