global using Cadenza.Common;
global using Cadenza.Library;
global using Cadenza.Domain;
global using Cadenza.Utilities;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cadenza.Source.Spotify.Repositories;
using Cadenza.Source.Spotify.Player;

namespace Cadenza.Source.Spotify;

public static class Startup
{
    public static IServiceCollection AddSpotifySource<TConfig>(this IServiceCollection services) where TConfig : class, ISpotifyApiConfig
    {
        return services
            .AddTransient<ISpotifyApiConfig, TConfig>()
            .AddTransient<ISourcePlayer, SpotifyPlayer>()
            .AddTransient<ISearchRepository, SpotifySearchRepository>()
            .AddTransient<IApiHelper, ApiHelper>()
            .AddTransient<IPlayerApi, PlayerApi>()
            .AddTransient<ISpotifyInterop, SpotifyInterop>();
    }

    public static IAudioPlayer GetSpotifyPlayer(this IServiceProvider services)
    {
        return services.GetRequiredService<SpotifyPlayer>();
    }

    
    public static IServiceCollection ConfigureSpotifyOverrides(this IServiceCollection services, IConfiguration config, params string[] sections)
    {
        return services.ConfigureOptions<SpotifyOverridesSettings>(config, sections);
    }
}
