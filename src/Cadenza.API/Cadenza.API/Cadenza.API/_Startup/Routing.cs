using Cadenza.API.Core.LastFM;

namespace Cadenza.API._Startup;

public static class Routing
{
    public static WebApplication AddRoutes(this WebApplication app)
    {
        app.MapGet("/Connect", () => "Connected");

        var lfmAuth = app.Services.GetRequiredService<LastFM.IAuthoriser>();
        app.MapGet(ApiEndpoints.LastFm.AuthUrl, (string redirectUri) => lfmAuth.GetAuthUrl(redirectUri));
        app.MapGet(ApiEndpoints.LastFm.CreateSession, (string token) => lfmAuth.CreateSession(token));

        var scrobbler = app.Services.GetRequiredService<IScrobbler>();
        app.MapPost(ApiEndpoints.LastFm.Scrobble, (Scrobble scrobble) => scrobbler.RecordPlay(scrobble));
        app.MapPost(ApiEndpoints.LastFm.UpdateNowPlaying, (Scrobble scrobble) => scrobbler.UpdateNowPlaying(scrobble));

        var favourites = app.Services.GetRequiredService<IFavourites>();
        app.MapGet(ApiEndpoints.LastFm.IsFavourite, (string artist, string title) => favourites.IsFavourite(artist, title));
        app.MapPost(ApiEndpoints.LastFm.Favourite, (Core.LastFM.Track track) => favourites.Favourite(track));
        app.MapPost(ApiEndpoints.LastFm.Unfavourite, (Core.LastFM.Track track) => favourites.Unfavourite(track));

        var history = app.Services.GetRequiredService<IHistory>();
        app.MapGet(ApiEndpoints.LastFm.RecentTracks, (int limit, int page) => history.GetRecentTracks(limit, page));
        app.MapGet(ApiEndpoints.LastFm.TopTracks, (HistoryPeriod period, int limit, int page) => history.GetPlayedTracks(period, limit, page));
        app.MapGet(ApiEndpoints.LastFm.TopAlbums, (HistoryPeriod period, int limit, int page) => history.GetPlayedAlbums(period, limit, page));
        app.MapGet(ApiEndpoints.LastFm.TopArtists, (HistoryPeriod period, int limit, int page) => history.GetPlayedArtists(period, limit, page));

        var authoriser = app.Services.GetRequiredService<Spotify.Interfaces.IAuthoriser>();
        app.MapGet(ApiEndpoints.Spotify.AuthHeader, () => authoriser.GetAuthHeader());
        app.MapGet(ApiEndpoints.Spotify.AuthUrl, (string state, string redirectUri) => authoriser.GetAuthUrl(state, redirectUri));

        return app;
    }
}