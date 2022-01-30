namespace Cadenza.API.Core;

public class Api
{
    public string BaseUrl { get; set; }
    public ApiEndpoints Endpoints { get; set; }
}

public class ApiEndpoints
{
    public string Favourite { get; set; }
    public string IsFavourite { get; set; }
    public string RecentTracks { get; set; }
    public string Scrobble { get; set; }
    public string TopAlbums { get; set; }
    public string TopArtists { get; set; }
    public string TopTracks { get; set; }
    public string UpdateNowPlaying { get; set; }
    public string Unfavourite { get; set; }
    public string SpotifyAuthHeader { get; set; }
    public string SpotifyAuthUrl { get; set; }
    public string SpotifyTokenUrl { get; set; }
    public string LastFmAuthUrl { get; set; }
    public string LastFmCreateSession { get; set; }
}