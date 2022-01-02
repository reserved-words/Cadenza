using Cadenza.Azure;
using Cadenza.Common;
using Cadenza.LastFM;

namespace Cadenza.API
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder RegisterDependencies(this WebApplicationBuilder builder)
        {
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

            return builder;
        }
    }
}
