namespace Cadenza.API.Core;

public static class ApiEndpoints
{
    public const string Connect = "/Connect";

    public static class LastFm
    {
        public const string Favourite = "/LastFm/Favourite";
        public const string IsFavourite = "/LastFm/IsFavourite";
        public const string RecentTracks = "/LastFm/RecentTracks";
        public const string Scrobble = "/LastFm/Scrobble";
        public const string TopAlbums = "/LastFm/TopAlbums";
        public const string TopArtists = "/LastFm/TopArtists";
        public const string TopTracks = "/LastFm/TopTracks";
        public const string UpdateNowPlaying = "/LastFm/UpdateNowPlaying";
        public const string Unfavourite = "/LastFm/Unfavourite";
        public const string AuthUrl = "/LastFm/AuthUrl";
        public const string CreateSession = "/LastFm/CreateSession";
    }

    public static class Spotify
    {
        public const string AuthHeader = "/Spotify/AuthHeader";
        public const string AuthUrl = "/Spotify/AuthUrl";
        public const string TokenUrl = "/Spotify/TokenUrl";
        public const string Populate = "/Spotify/Library/Populate";
        public const string SearchArtists = "/Spotify/Search/Artists";
        public const string SearchAlbums = "/Spotify/Search/Albums";
        public const string SearchTracks = "/Spotify/Search/Tracks";
        public const string SearchPlaylists = "/Spotify/Search/Playlists";
        public const string AddOverrides = "/Spotify/AddOverrides";
        public const string GetOverrides = "/Spotify/GetOverrides";
        public const string RemovedOverride = "/Spotify/RemovedOverride";
    }

}
