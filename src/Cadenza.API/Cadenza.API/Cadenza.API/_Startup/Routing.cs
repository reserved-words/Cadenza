﻿using Cadenza.API.Core.LastFM;

namespace Cadenza.API._Startup;

public static class Routing
{
    public static WebApplication AddRoutes(this WebApplication app)
    {
        app.MapGet("/Connect", () => "Connected");

        var lfmAuth = app.Services.GetRequiredService<LastFM.IAuthoriser>();
        app.MapGet(ApiEndpoints.AuthUrl, (string redirectUri) => lfmAuth.GetAuthUrl(redirectUri));
        app.MapGet(ApiEndpoints.CreateSession, (string token) => lfmAuth.CreateSession(token));

        var scrobbler = app.Services.GetRequiredService<IScrobbler>();
        app.MapPost(ApiEndpoints.Scrobble, (Scrobble scrobble) => scrobbler.RecordPlay(scrobble));
        app.MapPost(ApiEndpoints.UpdateNowPlaying, (Scrobble scrobble) => scrobbler.UpdateNowPlaying(scrobble));

        var favourites = app.Services.GetRequiredService<IFavourites>();
        app.MapGet(ApiEndpoints.IsFavourite, (string artist, string title) => favourites.IsFavourite(artist, title));
        app.MapPost(ApiEndpoints.Favourite, (Core.LastFM.Track track) => favourites.Favourite(track));
        app.MapPost(ApiEndpoints.Unfavourite, (Core.LastFM.Track track) => favourites.Unfavourite(track));

        var history = app.Services.GetRequiredService<IHistory>();
        app.MapGet(ApiEndpoints.RecentTracks, (int limit, int page) => history.GetRecentTracks(limit, page));
        app.MapGet(ApiEndpoints.TopTracks, (HistoryPeriod period, int limit, int page) => history.GetPlayedTracks(period, limit, page));
        app.MapGet(ApiEndpoints.TopAlbums, (HistoryPeriod period, int limit, int page) => history.GetPlayedAlbums(period, limit, page));
        app.MapGet(ApiEndpoints.TopArtists, (HistoryPeriod period, int limit, int page) => history.GetPlayedArtists(period, limit, page));

        return app;
    }
}