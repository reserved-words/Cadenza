using Cadenza.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Spotify;

public static class Startup
{
    public static IServiceCollection AddSpotify(this IServiceCollection services)
    {
        return services.AddTransient<IBuilder, Builder>()
            .AddTransient<Auth>();
    }

    public static IServiceCollection ConfigureSpotify(this IServiceCollection services, IConfiguration config, params string[] sectionPath)
    {
        return services.ConfigureOptions<SpotifySettings>(config, sectionPath);
    }
}