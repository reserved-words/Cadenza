global using Cadenza.Domain;
global using Cadenza.Utilities;

using System.Runtime.CompilerServices;
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
}