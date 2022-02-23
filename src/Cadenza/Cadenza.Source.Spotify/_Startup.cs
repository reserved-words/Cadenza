using Cadenza.Domain;
using Cadenza.Library;
using Cadenza.Source.Spotify.Interfaces;
using Cadenza.Source.Spotify.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Source.Spotify;

public static class _Startup
{
    public static IServiceCollection AddSpotifyCore(this IServiceCollection services)
    {
        return services
            .AddTransient<IAuthoriser, Authoriser>()
            .AddTransient<IInitialiser, Initialiser>()
            .AddApiRepositories<SpotifyApiRepositorySettings>(LibrarySource.Spotify);
    }
}