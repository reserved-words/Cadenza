global using Cadenza.Common;
global using Cadenza.Library;
global using Cadenza.Domain;
global using Cadenza.Utilities;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Source.Spotify;

public static class Startup
{
    public static IServiceCollection AddSpotifySource(this IServiceCollection services)
    {
        var sources = new Type[] { typeof(SpotifyOverridesLibrary), typeof(SpotifyApiLibrary) };
        var updaters = new Type[] { typeof(SpotifyOverridesUpdater) };

        return services
            .AddSingleton<SpotifyPlayer>()
            .AddTransient<ISpotifyApi, SpotifyApi>()
            .AddTransient<ISpotifyLibraryApi, SpotifyLibraryApi>()
            .AddTransient<ISpotifyPlayerApi, SpotifyPlayerApi>()
            .AddTransient<IOverridesService, SpotifyOverridesService>()
            .AddStaticSourceLibrary<SpotifyApiLibrary, SpotifyOverridesLibrary>(LibrarySource.Spotify);
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
