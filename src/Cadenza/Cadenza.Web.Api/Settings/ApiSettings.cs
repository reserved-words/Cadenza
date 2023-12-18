namespace Cadenza.Web.Api.Settings;

public class ApiSettings : ApiOptions<ApiEndpoints>
{
}

public class ApiEndpoints
{
    public string GroupingOptions { get; set; }

    public string RecentlyAddedAlbums { get; set; }
    public string RecentAlbumRequests { get; set; }
    public string RecentTagRequests { get; set; }
    public string RecentTracks { get; set; }
    public string UpdateNowPlaying { get; set; }
    public string RecordPlay { get; set; }
    public string TopAlbums { get; set; }
    public string TopArtists { get; set; }
    public string TopTracks { get; set; }

    public string UpdateAlbum { get; set; }
    public string UpdateArtist { get; set; }
    public string UpdateTrack { get; set; }

    public string ArtistImage { get; set; }
    public string AlbumArtwork { get; set; }

    public string LastFmAuthUrl { get; set; }
    public string LastFmCreateSession { get; set; }
    public string LastFmHasSession { get; set; }
    public string LastFmAlbumArtworkUrl { get; set; }

    public string Album { get; set; }
    public string AlbumFull { get; set; }
    public string AlbumGenre { get; set; }
    public string Artist { get; set; }
    public string ArtistFull { get; set; }
    public string Genre { get; set; }
    public string GroupingArtists { get; set; }
    public string Tag { get; set; }
    public string Track { get; set; }
    public string TrackFull { get; set; }

    public string PlayAlbum { get; set; }
    public string PlayArtist { get; set; }
    public string PlayGenre { get; set; }
    public string PlayGrouping { get; set; }
    public string PlayTag { get; set; }
    public string PlayTracks { get; set; }

    public string SearchAlbums { get; set; }
    public string SearchArtists { get; set; }
    public string SearchGenres { get; set; }
    public string SearchGroupings { get; set; }
    public string SearchTags { get; set; }
    public string SearchTracks { get; set; }

    public string Connect { get; set; }

    public string UnloveTrack { get; set; }
    public string LoveTrack { get; set; }
}