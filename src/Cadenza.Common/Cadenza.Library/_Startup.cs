global using Cadenza.Domain;
global using Cadenza.Utilities;
using Microsoft.Extensions.DependencyInjection;

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

    public static IServiceCollection AddSourceLibrary<TSource, TOverride>(this IServiceCollection services, LibrarySource librarySource)
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
            return new SourceLibrary(librarySource, combiner, source, ovrride);
        });

        return services;
    }

    public static IServiceCollection AddSourceLibrary<TSource>(this IServiceCollection services, LibrarySource librarySource)
        where TSource : class, IStaticSource
    {
        services.AddTransient<IMerger, Merger>();
        services.AddTransient<IStaticLibraryCacher, StaticLibraryCacher>();
        services.AddTransient<TSource>();

        services.AddTransient<ISourceLibrary>(sp =>
        {
            var combiner = sp.GetRequiredService<IStaticLibraryCacher>();
            var source = sp.GetRequiredService<TSource>();
            return new SourceLibrary(librarySource, combiner, source);
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