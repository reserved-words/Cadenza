using Cadenza.API.Wrapper.Core;
using Cadenza.Library;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.API.Wrapper.Spotify;

public static class _Startup
{
    public static IServiceCollection AddSpotifyCore(this IServiceCollection services)
    {
        return services
            .AddCoreServices()
            .AddTransient<IAuthoriser, Authoriser>()
            .AddTransient<IInitialiser, Initialiser>()
            .AddTransient<ISourceSearchRepository, SpotifySearchRepository>();
    }
}