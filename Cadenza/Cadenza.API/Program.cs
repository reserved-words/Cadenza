using Cadenza.API;
using Cadenza.Common;
using Cadenza.LastFM;

var builder = WebApplication.CreateBuilder(args)
    .BuildConfiguration()
    .DefineCors()
    .AddDocumentation();

var app = builder.Build()
    .ApplyCors()
    .UseDocumentation();

var base64Converter = new Base64Converter();
var hasher = new Hasher();
var config = builder.Configuration;
var build = new Builder(base64Converter);

var lfmConfig = new Cadenza.API.LastFm.Config(config);
var lfmSigner = new LastFmSigner(lfmConfig, hasher);

var lfmAuth = new LastFmAuth(lfmConfig, lfmSigner);
var httpClient = new Cadenza.Common.HttpClient(new System.Net.Http.HttpClient());
var client = new LastFmClient(httpClient, lfmConfig);
var authClient = new LastFmAuthorisedClient(httpClient, lfmConfig, lfmSigner);
var scrobbler = new Scrobbler(authClient);
var favouritesController = new FavouritesController(authClient);
var favouritesConsumer = new FavouritesConsumer(client, lfmConfig);

var spotify = new Cadenza.API.Spotify.Auth(config, build);

var azureConfig = new Cadenza.API.Azure.Config(config);
var azure = new Cadenza.Azure.SpotifyOverridesService(httpClient, azureConfig);

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

app.GenerateDocumentation();
app.Run();