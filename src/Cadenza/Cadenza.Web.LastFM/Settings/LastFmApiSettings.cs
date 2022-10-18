namespace Cadenza.Web.LastFM.Settings;

public class LastFmApiSettings : ApiOptions<LastFmApiEndpoints>
{
    public string RedirectUri { get; set; }
}

public class LastFmApiEndpoints
{
    public string AlbumArtworkUrl { get; set; }
    public string ArtistImageUrl { get; set; }
    public string AuthUrl { get; set; }
    public string CreateSession { get; set; }
    public string Unfavourite { get; set; }
    public string IsFavourite { get; set; }
    public string Favourite { get; set; }
    public string TopAlbums { get; set; }
    public string TopArtists { get; set; }
    public string TopTracks { get; set; }
    public string RecentTracks { get; set; }
    public string UpdateNowPlaying { get; set; }
    public string Scrobble { get; set; }
}


