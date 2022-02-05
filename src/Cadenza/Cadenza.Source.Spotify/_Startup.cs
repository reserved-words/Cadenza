global using Cadenza.Common;
global using Cadenza.Library;
global using Cadenza.Domain;
global using Cadenza.Utilities;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cadenza.Source.Spotify.Repositories;

namespace Cadenza.Source.Spotify;

public static class Startup
{
    public static IServiceCollection AddSpotifySource<TConfig>(this IServiceCollection services) where TConfig : class, ISpotifyApiConfig
    {
        return services
            .AddTransient<ISpotifyApiConfig, TConfig>()
            .AddTransient<ISourcePlayer, SpotifyPlayer>()
            .AddTransient<ISearchRepository, SpotifySearchRepository>()
            .AddTransient<ISpotifyApi, SpotifyApi>()
            .AddTransient<ISpotifyLibraryApi, SpotifyLibraryApi>()
            .AddTransient<ISpotifyPlayerApi, SpotifyPlayerApi>()
            .AddTransient<IOverridesService, SpotifyOverridesService>()
            .AddTransient<ISpotifyInterop, SpotifyInterop>()
            .AddLibrary<SpotifyApiLibrary, SpotifyOverridesLibrary>();
    }

    public static IAudioPlayer GetSpotifyPlayer(this IServiceProvider services)
    {
        return services.GetRequiredService<SpotifyPlayer>();
    }

    public static IOverridesService GetSpotifyOverrider(this IServiceProvider services)
    {
        return services.GetService<SpotifyOverridesService>();
    }

    public static IServiceCollection ConfigureSpotifyOverrides(this IServiceCollection services, IConfiguration config, params string[] sections)
    {
        return services.ConfigureOptions<SpotifyOverridesSettings>(config, sections);
    }
}
