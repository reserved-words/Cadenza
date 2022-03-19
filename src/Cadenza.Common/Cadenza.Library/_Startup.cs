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
        where TSource : class, IStaticLibrary
    {
        // call only once from API
        services.AddTransient<IValueMerger, ValueMerger>();
        services.AddTransient<IMerger, Merger>();
        services.AddTransient<IStaticLibrary, TSource>();
        services.AddTransient<ISourceFactory, SourceFactory>();
        services.AddSingleton<ILibrary, Library>();
        return services;
    }

    public static IServiceCollection AddLibrary<TSource, TOverride>(this IServiceCollection services)
        where TSource : class, IStaticLibrary
        where TOverride : class, IStaticLibrary
    {
        // call only once from API
        services.AddTransient<IValueMerger, ValueMerger>();
        services.AddTransient<IMerger, Merger>();
        services.AddTransient<IStaticLibrary, TSource>();
        services.AddTransient<IStaticLibrary, TOverride>();
        services.AddTransient<ISourceFactory, SourceFactory>();
        services.AddSingleton<ILibrary, Library>();

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
        // call multiple times from Blazor app
        // need to register multiple instances of each source repository interface

        return services
            .AddTransient<T>()
            .AddTransient<ISourceArtistRepository>(sp => GetApiRepository<T>(source, sp))
            .AddTransient<ISourceAlbumRepository>(sp => GetApiRepository<T>(source, sp))
            .AddTransient<ISourcePlayTrackRepository>(sp => GetApiRepository<T>(source, sp))
            .AddTransient<ISourceSearchRepository>(sp => GetApiRepository<T>(source, sp))
            .AddTransient<ISourceTrackRepository>(sp => GetApiRepository<T>(source, sp));
    }

    private static ApiRepository GetApiRepository<T>(LibrarySource source, IServiceProvider sp) where T : class, IApiRepositorySettings
    {
        return new ApiRepository(source, sp.GetService<IHttpHelper>(), sp.GetService<T>());
    }

    public static IServiceCollection AddMergedRepositories(this IServiceCollection services)
    {
        // call only once from Blazor app
        return services
            .AddTransient<IValueMerger, ValueMerger>()
            .AddTransient<IMerger, Merger>()
            .AddTransient<IMergedArtistRepository, MergedArtistRepository>()
            .AddTransient<IMergedAlbumRepository, MergedAlbumRepository>()
            .AddTransient<IMergedPlayTrackRepository, MergedPlayTrackRepository>()
            .AddTransient<IMergedTrackRepository, MergedTrackRepository>();
    }
}