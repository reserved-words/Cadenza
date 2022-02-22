using Cadenza.API.Core;
using Cadenza.API.Core.LastFM;
using Cadenza.Library;
using Cadenza.Spotify.API.Interfaces;

namespace Cadenza.API._Startup;

public static class Routing
{
    public static WebApplication AddRoutes(this WebApplication app)
    {
        app.MapGet("/Connect", () => "Connected");

        return app
            .AddLastFmRoutes()
            .AddSpotifyRoutes();
    }

    private static WebApplication AddLastFmRoutes(this WebApplication app)
    {
        var lfmAuth = app.Services.GetRequiredService<LastFM.IAuthoriser>();

        var history = app.Services.GetRequiredService<IHistory>();
        var scrobbler = app.Services.GetRequiredService<IScrobbler>();
        var favourites = app.Services.GetRequiredService<IFavourites>();

        app.MapGet(ApiEndpoints.LastFm.AuthUrl, (string redirectUri) => lfmAuth.GetAuthUrl(redirectUri));
        app.MapGet(ApiEndpoints.LastFm.CreateSession, (string token) => lfmAuth.CreateSession(token));

        app.MapPost(ApiEndpoints.LastFm.Scrobble, (Scrobble scrobble) => scrobbler.RecordPlay(scrobble));
        app.MapPost(ApiEndpoints.LastFm.UpdateNowPlaying, (Scrobble scrobble) => scrobbler.UpdateNowPlaying(scrobble));

        app.MapGet(ApiEndpoints.LastFm.IsFavourite, (string artist, string title) => favourites.IsFavourite(artist, title));
        app.MapPost(ApiEndpoints.LastFm.Favourite, (Core.LastFM.Track track) => favourites.Favourite(track));
        app.MapPost(ApiEndpoints.LastFm.Unfavourite, (Core.LastFM.Track track) => favourites.Unfavourite(track));

        app.MapGet(ApiEndpoints.LastFm.RecentTracks, (int limit, int page) => history.GetRecentTracks(limit, page));
        app.MapGet(ApiEndpoints.LastFm.TopTracks, (HistoryPeriod period, int limit, int page) => history.GetPlayedTracks(period, limit, page));
        app.MapGet(ApiEndpoints.LastFm.TopAlbums, (HistoryPeriod period, int limit, int page) => history.GetPlayedAlbums(period, limit, page));
        app.MapGet(ApiEndpoints.LastFm.TopArtists, (HistoryPeriod period, int limit, int page) => history.GetPlayedArtists(period, limit, page));

        return app;
    }

    private static WebApplication AddSpotifyRoutes(this WebApplication app)
    {
        var authoriser = app.Services.GetRequiredService<Spotify.IAuthoriser>();
        app.MapGet(ApiEndpoints.Spotify.AuthHeader, () => authoriser.GetAuthHeader());
        app.MapGet(ApiEndpoints.Spotify.TokenUrl, () => authoriser.GetTokenUrl());
        app.MapGet(ApiEndpoints.Spotify.AuthUrl, (string state, string redirectUri) => authoriser.GetAuthUrl(state, redirectUri));

        var apiToken = app.Services.GetRequiredService<IApiToken>();
        var library = app.Services.GetRequiredService<ILibrary>();
        var albumRepository = app.Services.GetRequiredService<IBaseAlbumRepository>();
        var artistRepository = app.Services.GetRequiredService<IBaseArtistRepository>();
        var playTrackRepository = app.Services.GetRequiredService<IBasePlayTrackRepository>();
        var searchRepository = app.Services.GetRequiredService<IBaseSearchRepository>();
        var trackRepository = app.Services.GetRequiredService<IBaseTrackRepository>();

        app.MapPost(ApiEndpoints.Spotify.Populate, async (string accessToken) =>
        {
            apiToken.SetAccessToken(accessToken);
            await library.Populate();
            await albumRepository.Populate();
            await artistRepository.Populate();
            await playTrackRepository.Populate();
            await searchRepository.Populate();
            await trackRepository.Populate();
        });

        app.MapGet(ApiEndpoints.Spotify.AllArtists, async (int page, int limit) =>
        {
            return await artistRepository.GetAllArtists(page, limit);
        });
        app.MapGet(ApiEndpoints.Spotify.AlbumArtists, (int page, int limit) => artistRepository.GetAlbumArtists(page, limit));
        app.MapGet(ApiEndpoints.Spotify.TrackArtists, (int page, int limit) => artistRepository.GetTrackArtists(page, limit));
        app.MapGet(ApiEndpoints.Spotify.Artist, (string id) => artistRepository.GetArtist(id));
        app.MapGet(ApiEndpoints.Spotify.ArtistAlbums, (string id, int page, int limit) => artistRepository.GetAlbums(id, page, limit));

        app.MapGet(ApiEndpoints.Spotify.PlayTracks, (int page, int limit) => playTrackRepository.GetAll(page, limit));
        app.MapGet(ApiEndpoints.Spotify.PlayArtist, (string id, int page, int limit) => playTrackRepository.GetByArtist(id, page, limit));
        app.MapGet(ApiEndpoints.Spotify.PlayAlbum, (string id, int page, int limit) => playTrackRepository.GetByAlbum(id, page, limit));

        app.MapGet(ApiEndpoints.Spotify.SearchArtists, (int page, int limit) => 
        {
            return searchRepository.GetSearchArtists(page, limit);
        });
        app.MapGet(ApiEndpoints.Spotify.SearchAlbums, (int page, int limit) => searchRepository.GetSearchAlbums(page, limit));
        app.MapGet(ApiEndpoints.Spotify.SearchPlaylists, (int page, int limit) => searchRepository.GetSearchPlaylists(page, limit));
        app.MapGet(ApiEndpoints.Spotify.SearchTracks, (int page, int limit) => searchRepository.GetSearchTracks(page, limit));

        app.MapGet(ApiEndpoints.Spotify.Track, (string id) => trackRepository.GetTrack(id));
        app.MapGet(ApiEndpoints.Spotify.Album, (string id) => albumRepository.GetAlbum(id));
        app.MapGet(ApiEndpoints.Spotify.AlbumTracks, (string id) => albumRepository.GetAlbumTracks(id));

        var azure = app.Services.GetRequiredService<SpotifyOverridesService>();
        app.MapGet(ApiEndpoints.Spotify.GetOverrides, () => azure.GetOverrides());
        app.MapGet(ApiEndpoints.Spotify.AddOverrides, (r) => throw new NotImplementedException());
        app.MapGet(ApiEndpoints.Spotify.RemovedOverride, (r) => throw new NotImplementedException());

        return app;
    }
}