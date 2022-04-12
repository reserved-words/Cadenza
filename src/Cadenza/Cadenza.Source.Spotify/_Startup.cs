using Cadenza.Core.Interfaces;
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
            .AddSpotifyApi<SpotifyTokenProvider>()
            .AddTransient<IDeviceHelper, DeviceHelper>()
            .AddTransient<IPlayerService, PlayerService>()
            .AddTransient<IProgressService, ProgressService>()
            .AddTransient<ITokenProvider, SpotifyTokenProvider>()
            .AddTransient<ISpotifyInterop, SpotifyInterop>()
            .AddTransient<ISourcePlayer, SpotifyPlayer>()
            .AddTransient<IConnectionTaskBuilder, SpotifyConnectionTaskBuilder>()
            .AddTransient<IAuthoriser, Authoriser>()
            .AddTransient<ISpotifySession, SpotifySession>();
;
    }
}