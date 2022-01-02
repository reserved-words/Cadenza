using Cadenza.API;
using Cadenza.Azure;
using Cadenza.LastFM;

namespace Cadenza.API
{
    public static class Routing
    {
        public static WebApplication AddRoutes(this WebApplication app)
        {
            var lfmAuth = app.Services.GetRequiredService<ILastFmAuth>();
            var scrobbler = app.Services.GetRequiredService<Scrobbler>();
            var favouritesController = app.Services.GetRequiredService<FavouritesController>();
            var favouritesConsumer = app.Services.GetRequiredService<FavouritesConsumer>();
            var spotify = app.Services.GetRequiredService<Cadenza.API.Spotify.Auth>();
            var azure = app.Services.GetRequiredService<SpotifyOverridesService>();

            app.MapGet("/LastFm/SessionKeyUrl", (string token) => lfmAuth.GetSessionKeyUrl(token));
            app.MapGet("/LastFm/AuthUrl", (string redirectUri) => lfmAuth.GetAuthUrl(redirectUri));
            app.MapGet("/LastFm/IsFavourite", (string artist, string title) => favouritesConsumer.IsFavourite(artist, title));

            app.MapPost("/LastFm/Scrobble", (Scrobble scrobble) => scrobbler.RecordPlay(scrobble));
            app.MapPost("/LastFm/UpdateNowPlaying", (Scrobble scrobble) => scrobbler.UpdateNowPlaying(scrobble));
            app.MapPost("/LastFm/Favourite", (Cadenza.LastFM.Track track) => favouritesController.Favourite(track));
            app.MapPost("/LastFm/Unfavourite", (Cadenza.LastFM.Track track) => favouritesController.Unfavourite(track));

            app.MapGet("/Spotify/AuthHeader", () => spotify.GetAuthHeader());
            app.MapGet("/Spotify/TokenUrl", () => spotify.GetTokenUrl());
            app.MapGet("/Spotify/AuthUrl", (string redirectUri) => spotify.GetAuthUrl(redirectUri));

            app.MapGet("/Azure/GetSpotifyOverrides", () => azure.GetOverrides());
            app.MapGet("/Azure/AddOverrides", (r) => throw new NotImplementedException());
            app.MapGet("/Azure/RemoveOverride", (r) => throw new NotImplementedException());

            return app;
        }
    }
}
