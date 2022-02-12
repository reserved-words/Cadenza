global using Cadenza.Domain;
global using Cadenza.Utilities;

using System.Runtime.CompilerServices;
using Cadenza.Library.Libraries;
using Cadenza.Library.Repositories;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("Cadenza.Library.Tests")]

namespace Cadenza.Library;

public static class _Startup
{
    public static IServiceCollection AddLibrary<TSource>(this IServiceCollection services)
        where TSource : class, IStaticLibrary
    {
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
        services.AddSingleton<IBaseArtistRepository, BaseArtistRepository>();
        services.AddSingleton<IBaseAlbumRepository, BaseAlbumRepository>();
        services.AddSingleton<IBasePlayTrackRepository, BasePlayTrackRepository>();
        services.AddSingleton<IBaseSearchRepository, BaseSearchRepository>();
        services.AddSingleton<IBaseTrackRepository, BaseTrackRepository>();
        return services;
    }

    public static IServiceCollection AddApiRepositories<T>(this IServiceCollection services, LibrarySource source) where T : class, IApiRepositorySettings
    {
        return services
            .AddTransient<ISource>(sp => new SourceProvider(source))
            .AddTransient<IApiRepositorySettings, T>()
            .AddTransient<ISourceArtistRepository, ApiRepository>()
            .AddTransient<ISourceAlbumRepository, ApiRepository>()
            .AddTransient<ISourcePlayTrackRepository, ApiRepository>()
            .AddTransient<ISourceSearchRepository, ApiRepository>()
            .AddTransient<ISourceTrackRepository, ApiRepository>()
            .AddTransient<IMergedArtistRepository, MergedArtistRepository>()
            .AddTransient<IMergedAlbumRepository, MergedAlbumRepository>()
            .AddTransient<IMergedPlayTrackRepository, MergedPlayTrackRepository>()
            .AddTransient<IMergedTrackRepository, MergedTrackRepository>();
    }
}