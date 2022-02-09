global using Cadenza.Domain;
global using Cadenza.Utilities;

using System.Runtime.CompilerServices;
using Cadenza.Library.Libraries;
using Cadenza.Library.Repositories;
using Microsoft.Extensions.Configuration;
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

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IArtistRepository, ArtistRepository>();
        services.AddTransient<IPlayTrackRepository, PlayTrackRepository>();
        services.AddTransient<ISearchRepository, SearchRepository>();
        services.AddTransient<ITrackRepository, TrackRepository>();
        return services;
    }

    public static IServiceCollection AddApiRepository(this IServiceCollection services, LibrarySource source, IConfiguration config, string sectionPath)
    {
        services
            .AddTransient<ISource>(sp => new SourceProvider(source))
            .AddTransient<ISourceArtistRepository, ApiRepository>()
            .AddTransient<ISourcePlayTrackRepository, ApiRepository>()
            .AddTransient<ISourceSearchRepository, ApiRepository>()
            .AddTransient<ISourceTrackRepository, ApiRepository>();

        var section = config.GetSection(sectionPath);
        services.Configure<ApiRepositorySettings>(section);
        return services;
    }
}