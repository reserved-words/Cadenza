global using Cadenza.Domain;
global using Cadenza.Utilities;

using System.Runtime.CompilerServices;
using Cadenza.Library.Repositories;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Cadenza.Library.Tests")]

namespace Cadenza.Library;

public static class _Startup
{
    public static IServiceCollection AddLibrary<TSource>(this IServiceCollection services)
        where TSource : class, ILibrary
    {
        // call only once from API
        services.AddTransient<IValueMerger, ValueMerger>();
        services.AddTransient<IMerger, Merger>();
        services.AddTransient<ILibrary, TSource>();
        return services;
    }

    public static IServiceCollection AddBaseRepositories(this IServiceCollection services)
    {
        // call only once from each API
        services.AddTransient<IValueMerger, ValueMerger>();
        services.AddTransient<IMerger, Merger>();
        services.AddSingleton<IArtistCache, ArtistCache>();
        services.AddSingleton<IAlbumCache, AlbumCache>();
        services.AddSingleton<IPlayTrackCache, PlayTrackCache>();
        services.AddSingleton<ISearchCache, SearchCache>();
        services.AddSingleton<ITrackCache, TrackCache>();
        return services;
    }

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