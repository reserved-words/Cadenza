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
        var lfmAuth = app.Services.GetRequiredService<ILastFmAuth>();

        var history = app.Services.GetRequiredService<History>();
        var scrobbler = app.Services.GetRequiredService<Scrobbler>();
        var favourites = app.Services.GetRequiredService<Favourites>();

        app.MapGet("/LastFm/SessionKeyUrl", (string token) => lfmAuth.GetSessionKeyUrl(token));
        app.MapGet("/LastFm/AuthUrl", (string redirectUri) => lfmAuth.GetAuthUrl(redirectUri));

        app.MapPost("/LastFm/Scrobble", (Scrobble scrobble) => scrobbler.RecordPlay(scrobble));
        app.MapPost("/LastFm/UpdateNowPlaying", (Scrobble scrobble) => scrobbler.UpdateNowPlaying(scrobble));
        app.MapPost("/LastFm/Favourite", (LastFM.Track track) => favourites.Favourite(track));
        
        app.MapGet("/LastFm/IsFavourite", (string artist, string title) => favourites.IsFavourite(artist, title));
        app.MapGet("/LastFm/RecentTracks", (int limit, int page) => history.GetRecentTracks(limit, page));
        app.MapGet("/LastFm/TopTracks", (HistoryPeriod period, int limit, int page) => history.GetPlayedTracks(period, limit, page));
        app.MapGet("/LastFm/TopAlbums", (HistoryPeriod period, int limit, int page) => history.GetPlayedAlbums(period, limit, page));
        app.MapGet("/LastFm/TopArtists", (HistoryPeriod period, int limit, int page) => history.GetPlayedArtists(period, limit, page));

        return app;
    }

    private static WebApplication AddSpotifyRoutes(this WebApplication app)
    {
        var spotify = app.Services.GetRequiredService<Auth>();
        app.MapGet("/Spotify/AuthHeader", () => spotify.GetAuthHeader());
        app.MapGet("/Spotify/TokenUrl", () => spotify.GetTokenUrl());
        app.MapGet("/Spotify/AuthUrl", (string redirectUri) => spotify.GetAuthUrl(redirectUri));
        return app;
    }
}