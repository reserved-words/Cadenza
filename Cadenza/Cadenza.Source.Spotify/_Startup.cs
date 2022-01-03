global using Cadenza.Common;
global using Cadenza.Library;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Source.Spotify;

public static class Startup
{
    public static IServiceCollection AddSpotifySource(this IServiceCollection services)
    {
        var spotifyCache = new Cache();
        
        return services
            .AddSingleton<SpotifyPlayer>()
            .AddTransient<ISpotifyApi, SpotifyApi>()
            .AddTransient<ISpotifyLibraryApi, SpotifyLibraryApi>()
            .AddTransient<ISpotifyPlayerApi, SpotifyPlayerApi>()
            .AddTransient<IOverridesService, SpotifyOverridesService>()
            .AddTransient<SpotifyOverridesService>()
            .AddTransient<SpotifyOverrides>()
            .AddTransient<SpotifyApiLibrary>()
            .AddTransient<SpotifyLibrary>(sp =>
            {
                var merger = sp.GetRequiredService<IMerger>();
                var staticSources = new List<IStaticSource> {
                        sp.GetService<SpotifyOverrides>(),
                        sp.GetService<SpotifyApiLibrary>()
                };
                var combinedLibrary = new CombinedStaticLibrary(spotifyCache, merger, staticSources);
                return new SpotifyLibrary(
                    combinedLibrary,
                    sp.GetService<ISpotifyLibraryApi>(),
                    sp.GetService<IIdGenerator>());
            })
            .AddTransient<SpotifyUpdater>(sp => new SpotifyUpdater(
                new LibraryUpdater(sp.GetRequiredService<IMerger>(), spotifyCache),
                sp.GetRequiredService<IOverridesService>()
                )); ;
    }

    public static IAudioPlayer GetSpotifyPlayer(this IServiceProvider services)
    {
        return services.GetRequiredService<SpotifyPlayer>();
    }

    public static ISourceRepository GetSpotifyRepository(this IServiceProvider services)
    {
        return services.GetRequiredService<SpotifyLibrary>();
    }

    public static ISourceLibraryUpdater GetSpotifyUpdater(this IServiceProvider services)
    {
        return services.GetRequiredService<SpotifyUpdater>();
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
