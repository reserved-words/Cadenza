global using Cadenza.Domain;
global using Cadenza.Utilities;

using System.Runtime.CompilerServices;
using Cadenza.Domain.Enums;
using Cadenza.Library.Repositories;
using Cadenza.Utilities.Interfaces;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Cadenza.Library.Tests")]

namespace Cadenza.Library;

public static class _Startup
{
    public static IServiceCollection AddApiRepositories<T>(this IServiceCollection services, LibrarySource source) where T : class, IApiRepositorySettings
    {
        return services
            .AddTransient<T>()
            .AddTransient<IArtistRepository>(sp => GetApiRepository<T>(sp))
            .AddTransient<IAlbumRepository>(sp => GetApiRepository<T>(sp))
            .AddTransient<IPlayTrackRepository>(sp => GetApiRepository<T>(sp))
            .AddTransient<ISearchRepository>(sp => GetApiRepository<T>(sp))
            .AddTransient<ITrackRepository>(sp => GetApiRepository<T>(sp));
    }

    private static ApiRepository GetApiRepository<T>(IServiceProvider sp) where T : class, IApiRepositorySettings
    {
        return new ApiRepository(sp.GetService<IHttpHelper>(), sp.GetService<T>());
    }
}