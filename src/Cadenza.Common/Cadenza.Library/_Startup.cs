global using Cadenza.Domain;
global using Cadenza.Utilities;

using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("Cadenza.Library.Tests")]

namespace Cadenza.Library;

public static class _Startup
{
    public static IServiceCollection AddLibrary<TSource>(this IServiceCollection services)
        where TSource : class, IStaticSource
    {
        services.AddTransient<IMerger, Merger>();
        services.AddTransient<IStaticLibraryCacher, StaticLibraryCacher>();
        services.AddTransient<TSource>();

        services.AddTransient<ILibrary>(sp =>
        {
            var combiner = sp.GetRequiredService<IStaticLibraryCacher>();
            var source = sp.GetRequiredService<TSource>();
            return new Library(combiner, source);
        });

        return services;
    }

    public static IServiceCollection AddLibrary<TSource, TOverride>(this IServiceCollection services)
        where TSource : class, IStaticSource
        where TOverride : class, IStaticSource
    {
        services.AddTransient<IMerger, Merger>();
        services.AddTransient<IStaticLibraryCacher, StaticLibraryCacher>();
        services.AddTransient<TSource>();
        services.AddTransient<TOverride>();

        services.AddTransient<ILibrary>(sp =>
        {
            var combiner = sp.GetRequiredService<IStaticLibraryCacher>();
            var source = sp.GetRequiredService<TSource>();
            var ovrride = sp.GetRequiredService<TOverride>();
            return new Library(combiner, source, ovrride);
        });

        return services;
    }

    public static IServiceCollection AddStaticSourceLibrary<TSource, TOverride>(this IServiceCollection services, LibrarySource librarySource)
        where TSource : class, IStaticSource
        where TOverride : class, IStaticSource
    {
        services.AddTransient<IMerger, Merger>();
        services.AddTransient<IStaticLibraryCacher, StaticLibraryCacher>();
        services.AddTransient<TSource>();
        services.AddTransient<TOverride>();

        services.AddTransient<ISourceLibrary>(sp =>
        {
            var combiner = sp.GetRequiredService<IStaticLibraryCacher>();
            var source = sp.GetRequiredService<TSource>();
            var ovrride = sp.GetRequiredService<TOverride>();
            return new StaticSourceLibrary(librarySource, combiner, source, ovrride);
        });

        return services;
    }

    public static IServiceCollection AddStaticSourceLibrary<TSource>(this IServiceCollection services, LibrarySource librarySource)
        where TSource : class, IStaticSource
    {
        services.AddTransient<IMerger, Merger>();
        services.AddTransient<IStaticLibraryCacher, StaticLibraryCacher>();
        services.AddTransient<TSource>();

        services.AddTransient<ISourceLibrary>(sp =>
        {
            var combiner = sp.GetRequiredService<IStaticLibraryCacher>();
            var source = sp.GetRequiredService<TSource>();
            return new StaticSourceLibrary(librarySource, combiner, source);
        });

        return services;
    }

    public static IServiceCollection AddDynamicSourceLibrary<TLibrary>(this IServiceCollection services, LibrarySource librarySource)
        where TLibrary : class, ILibrary
    {
        services.AddTransient<IMerger, Merger>();
        services.AddTransient<IStaticLibraryCacher, StaticLibraryCacher>();
        services.AddTransient<TLibrary>();

        services.AddTransient<ISourceLibrary>(sp =>
        {
            var baseLibrary = sp.GetRequiredService<TLibrary>();
            return new DynamicSourceLibrary(librarySource, baseLibrary);
        });

        return services;
    }


    //private static IUpdater GetCombinedUpdater(this IServiceProvider sp)
    //{
    //    var updaters = sp.GetRequiredService<IEnumerable<ISourceUpdater>>();
    //    return new CombinedUpdater(updaters);
    //}

    //private static void RegisterList<IInterface>(this IServiceCollection services, Type[] types) where IInterface : class
    //{
    //    services.AddTransient(sp => types
    //        .Select(t => sp.GetService(t) as IInterface));
    //}

    //private static void RegisterTypes(this IServiceCollection services, Type[] types)
    //{
    //    foreach (var type in types)
    //    {
    //        services.AddTransient(type);
    //    }
    //}

    //private static void VerifyTypes<IInterface>(Type[] types)
    //{
    //    foreach (var source in types)
    //    {
    //        if (!source.IsAssignableTo(typeof(IInterface)))
    //        {
    //            throw new InvalidOperationException($"The type {source.Name} does not implement the interface {typeof(IInterface).Name}");
    //        }
    //    }
    //}
}