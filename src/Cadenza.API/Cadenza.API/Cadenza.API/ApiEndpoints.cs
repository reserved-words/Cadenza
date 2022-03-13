namespace Cadenza.API;

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

        public const string Artist = "/Spotify/Artist";
        public const string ArtistAlbums = "/Spotify/Artist/Albums";
        public const string AllArtists = "/Spotify/Artists";
        public const string AlbumArtists = "/Spotify/Artists/Album";
        public const string TrackArtists = "/Spotify/Artists/Track";
        public const string GroupingArtists = "/Spotify/Artists/Grouping";
        public const string GenreArtists = "/Spotify/Artists/Genre";

        public const string PlayTracks = "/Spotify/Play/Tracks";
        public const string PlayArtist = "/Spotify/Play/Artist";
        public const string PlayAlbum = "/Spotify/Play/Album";
        public const string PlayGenre = "/Spotify/Play/Genre";
        public const string PlayGrouping = "/Spotify/Play/Grouping";

        public const string SearchArtists = "/Spotify/Search/Artists";
        public const string SearchAlbums = "/Spotify/Search/Albums";
        public const string SearchTracks = "/Spotify/Search/Tracks";
        public const string SearchPlaylists = "/Spotify/Search/Playlists";
        public const string SearchGroupings = "/Spotify/Search/Groupings";
        public const string SearchGenres = "/Spotify/Search/Genres";

        public const string Track = "/Spotify/Track";
        public const string Album = "/Spotify/Album";
        public const string AlbumTracks = "/Spotify/Album/Tracks";

        public const string AddOverrides = "/Spotify/AddOverrides";
        public const string GetOverrides = "/Spotify/GetOverrides";
        public const string RemovedOverride = "/Spotify/RemovedOverride";

        public const string UpdateArtist = "/Spotify/Update/Artist";
    }

}
