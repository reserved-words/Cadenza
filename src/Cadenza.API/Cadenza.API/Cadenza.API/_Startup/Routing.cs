using Cadenza.API.Core;
using Cadenza.API.Core.LastFM;

namespace Cadenza.API;

public static class Routing
{
    public static WebApplication AddRoutes(this WebApplication app)
    {
        return app
            .AddAzureRoutes()
            .AddLastFmRoutes()
            .AddSpotifyRoutes();
    }

    private static WebApplication AddAzureRoutes(this WebApplication app)
    {
        var azure = app.Services.GetRequiredService<SpotifyOverridesService>();
        app.MapGet("/Azure/GetSpotifyOverrides", () => azure.GetOverrides());
        app.MapGet("/Azure/AddOverrides", (r) => throw new NotImplementedException());
        app.MapGet("/Azure/RemoveOverride", (r) => throw new NotImplementedException());
        return app;
    }

    private static WebApplication AddLastFmRoutes(this WebApplication app)
    {
        var lfmAuth = app.Services.GetRequiredService<IAuthoriser>();

        var history = app.Services.GetRequiredService<IHistory>();
        var scrobbler = app.Services.GetRequiredService<IScrobbler>();
        var favourites = app.Services.GetRequiredService<IFavourites>();

        app.MapGet(ApiEndpoints.LastFmAuthUrl, (string redirectUri) => lfmAuth.GetAuthUrl(redirectUri));
        app.MapGet(ApiEndpoints.LastFmCreateSession, (string token) => lfmAuth.CreateSession(token));

        app.MapPost(ApiEndpoints.Scrobble, (Scrobble scrobble) => scrobbler.RecordPlay(scrobble));
        app.MapPost(ApiEndpoints.UpdateNowPlaying, (Scrobble scrobble) => scrobbler.UpdateNowPlaying(scrobble));

        app.MapGet(ApiEndpoints.IsFavourite, (string artist, string title) => favourites.IsFavourite(artist, title));
        app.MapPost(ApiEndpoints.Favourite, (Core.LastFM.Track track) => favourites.Favourite(track));
        app.MapPost(ApiEndpoints.Unfavourite, (Core.LastFM.Track track) => favourites.Unfavourite(track));

        app.MapGet(ApiEndpoints.RecentTracks, (int limit, int page) => history.GetRecentTracks(limit, page));
        app.MapGet(ApiEndpoints.TopTracks, (HistoryPeriod period, int limit, int page) => history.GetPlayedTracks(period, limit, page));
        app.MapGet(ApiEndpoints.TopAlbums, (HistoryPeriod period, int limit, int page) => history.GetPlayedAlbums(period, limit, page));
        app.MapGet(ApiEndpoints.TopArtists, (HistoryPeriod period, int limit, int page) => history.GetPlayedArtists(period, limit, page));

        return app;
    }

    private static WebApplication AddSpotifyRoutes(this WebApplication app)
    {
        var spotify = app.Services.GetRequiredService<Core.Spotify.IAuthoriser>();
        app.MapGet(ApiEndpoints.SpotifyAuthHeader, () => spotify.GetAuthHeader());
        app.MapGet(ApiEndpoints.SpotifyTokenUrl, () => spotify.GetTokenUrl());
        app.MapGet(ApiEndpoints.SpotifyAuthUrl, (string redirectUri) => spotify.GetAuthUrl(redirectUri));
        return app;
    }
}