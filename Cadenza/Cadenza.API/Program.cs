using Cadenza.API;
using Cadenza.Azure;
using Cadenza.Common;
using Cadenza.LastFM;

var builder = WebApplication.CreateBuilder(args)
    .BuildConfiguration()
    .DefineCors()
    .AddDocumentation();

builder.Services.Configure<LastFmSettings>(builder.Configuration.GetSection("LastFm"));
builder.Services.Configure<AzureSettings>(builder.Configuration.GetSection("Azure"));
builder.Services.Configure<Cadenza.API.Spotify.Settings>(builder.Configuration.GetSection("Spotify"));

builder.Services.AddTransient<IBase64Converter, Base64Converter>();
builder.Services.AddTransient<IHasher, Hasher>();
builder.Services.AddTransient<IBuilder, Builder>();

builder.Services.AddTransient<ILastFmSigner, LastFmSigner>();
builder.Services.AddTransient<ILastFmAuth, LastFmAuth>();
builder.Services.AddTransient<System.Net.Http.HttpClient>();
builder.Services.AddTransient<IHttpClient, Cadenza.Common.HttpClient>();

builder.Services.AddTransient<ILastFmClient, LastFmClient>();
builder.Services.AddTransient<ILastFmAuthorisedClient, LastFmAuthorisedClient>();

builder.Services.AddTransient<Scrobbler>();
builder.Services.AddTransient<FavouritesController>();
builder.Services.AddTransient<FavouritesConsumer>();

builder.Services.AddTransient<Cadenza.API.Spotify.Auth>();
builder.Services.AddTransient<SpotifyOverridesService>();

var app = builder.Build()
    .ApplyCors()
    .UseDocumentation();

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

app.GenerateDocumentation();
app.Run();