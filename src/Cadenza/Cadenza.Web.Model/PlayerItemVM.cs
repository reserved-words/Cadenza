namespace Cadenza.Web.Model;

public record PlayerItemVM(PlayerItemType Type, string Id, string Name, string Artist, string Album, string AlbumDisplay);

//public record SearchableAlbumVM : PlayerItemVM
//{
//    public SearchableAlbumVM(string id, string title, string artist)
//        : base(PlayerItemType.Album, id, title, artist, null, null) { }

//    public SearchableAlbumVM(AlbumDetailsVM album)
//        : this(album.Id.ToString(), album.Title, album.ArtistName) { }
//}

//public record SearchableArtistVM : PlayerItemVM
//{
//    public SearchableArtistVM(string id, string name)
//        : base(PlayerItemType.Artist, id, name, null, null, null) { }

//    public SearchableArtistVM(ArtistDetailsVM artist)
//        : this(artist.Id.ToString(), artist.Name) { }
//}

//public record SearchableTagVM : PlayerItemVM
//{
//    public SearchableTagVM(string id)
//        : base(PlayerItemType.Tag, id, id, null, null, null) { }
//}

//public record SearchableTrackVM : PlayerItemVM
//{
//    public SearchableTrackVM(string id, string title, string artist, string album, string albumArtist)
//        : base(PlayerItemType.Track, id, title, artist, album, album + (albumArtist == artist ? "" : $" ({albumArtist})")) { }

//    public SearchableTrackVM(TrackDetailsVM track, AlbumDetailsVM album)
//        : this(track.Id.ToString(), track.Title, track.ArtistName, album.Title, album.ArtistName) { }
//}

//public record SearchableGroupingVM : PlayerItemVM
//{
//    public SearchableGroupingVM(GroupingVM grouping)
//        : base(PlayerItemType.Grouping, grouping.Id.ToString(), grouping.Name, null, null, null) { }
//}

//public record SearchableGenreVM : PlayerItemVM
//{
//    public SearchableGenreVM(string id)
//        : base(PlayerItemType.Genre, id, id, null, null, null) { }
//}

