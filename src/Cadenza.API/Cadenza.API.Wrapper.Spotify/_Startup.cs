using Cadenza.Domain;
using Cadenza.Library;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.API.Wrapper.Spotify;

public static class _Startup
{
    public static IServiceCollection AddSpotifyCore(this IServiceCollection services)
    {
        return services
            .AddTransient<IAuthoriser, Authoriser>()
            .AddTransient<IInitialiser, Initialiser>()
            .AddApiRepositories<SpotifyApiSettings>(LibrarySource.Spotify);
    }
}