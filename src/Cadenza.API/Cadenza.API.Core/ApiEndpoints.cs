namespace Cadenza.API.Core;

public static class ApiEndpoints
{
    public static string Favourite => "/LastFm/Favourite";
    public static string IsFavourite => "/LastFm/IsFavourite";
    public static string RecentTracks => "/LastFm/RecentTracks";
    public static string Scrobble => "/LastFm/Scrobble";
    public static string TopAlbums => "/LastFm/TopAlbums";
    public static string TopArtists => "/LastFm/TopArtists";
    public static string TopTracks => "/LastFm/TopTracks";
    public static string UpdateNowPlaying => "/LastFm/UpdateNowPlaying";
    public static string Unfavourite => "/LastFm/Unfavourite";
    public static string SpotifyAuthHeader => "/Spotify/AuthHeader";
    public static string SpotifyAuthUrl => "/Spotify/AuthUrl";
    public static string SpotifyTokenUrl => "/Spotify/TokenUrl";
    public static string LastFmAuthUrl => "/LastFm/AuthUrl";
    public static string LastFmCreateSession => "/LastFm/CreateSession";
}