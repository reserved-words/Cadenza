namespace Cadenza.Web.Database.Settings;

public class DatabaseApiSettings : ApiOptions<DatabaseApiEndpoints>
{
}

public class DatabaseApiEndpoints
{
    public string Connect { get; set; }

    public string RecentAlbumRequests { get; set; }
    public string RecentTagRequests { get; set; }

    public string GroupingOptions { get; set; }

    public string UpdateAlbumTracks { get; set; }
    public string UpdateAlbum { get; set; }
    public string UpdateArtist { get; set; }
    public string UpdateTrack { get; set; }

    public string ArtistImage { get; set; }
    public string AlbumArtwork { get; set; }
    public string AlbumsFeaturingArtist { get; set; }

    public string Artist { get; set; }
    public string ArtistAlbums { get; set; }
    public string GroupingArtists { get; set; }
    public string GenreArtists { get; set; }
    public string Tag { get; set; }

    public string PlayAlbum { get; set; }
    public string PlayArtist { get; set; }
    public string PlayGenre { get; set; }
    public string PlayGrouping { get; set; }
    public string PlayTag { get; set; }
    public string PlayTracks { get; set; }

    public string SearchArtists { get; set; }
    public string SearchAlbums { get; set; }
    public string SearchGenres { get; set; }
    public string SearchGroupings { get; set; }
    public string SearchPlaylists { get; set; }
    public string SearchTags { get; set; }
    public string SearchTracks { get; set; }

    public string Track { get; set; }
    public string Album { get; set; }
    public string AlbumTracks { get; set; }

    public string AlbumArtworkUrl { get; set; }
    public string ArtistImageUrl { get; set; }
}