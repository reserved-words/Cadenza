using Cadenza.Core.Interfaces;
using Cadenza.Domain;
using Cadenza.Library;
using Cadenza.Source.Spotify.Api;
using Cadenza.Source.Spotify.Api.Interfaces;
using Cadenza.Source.Spotify.Interfaces;
using Cadenza.Source.Spotify.Services;
using Cadenza.Source.Spotify.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Source.Spotify;

public static class _Startup
{
    public static IServiceCollection AddSpotifySource(this IServiceCollection services, IConfiguration config, string apiSectionName)
    {
        services.Configure<SpotifyApiSettings>(config.GetSection(apiSectionName));

        return services
            .AddTransient<IInitialiser, Initialiser>()
            .AddTransient<IApiHelper, ApiHelper>()
            .AddTransient<IDeviceHelper, DeviceHelper>()
            .AddTransient<IDevicesApi, DevicesApi>()
            .AddTransient<IPlayerApi, PlayerApi>()
            .AddTransient<IProgressApi, ProgressApi>()
            .AddTransient<ISpotifyAuthHelper, SpotifyAuthHelper>()
            .AddTransient<ISpotifyInterop, SpotifyInterop>()
            .AddTransient<ISourcePlayer, SpotifyPlayer>()
            .AddTransient<IConnectionTaskBuilder, SpotifyConnectionTaskBuilder>()
            //.AddApiRepositories<SpotifyApiRepositorySettings>(LibrarySource.Spotify)
            //.AddTransient<IBuilder, Builder>()
            .AddTransient<IAuthoriser, Authoriser>()
            .AddTransient<IApiCaller, ApiCaller>()
            .AddSingleton<IApiToken, ApiToken>();
;
    }
}