global using Cadenza.Domain;
global using Cadenza.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Library;

public static class _Startup
{
    public static IServiceCollection AddCombinedLibrary(this IServiceCollection services, Type[] sources, Type[] updaters)
    {
        VerifyTypes<IStaticSource>(sources);
        VerifyTypes<ISourceUpdater>(updaters);

        services.AddSingleton<ICache, Cache>();
        services.AddTransient<IMerger, Merger>();
        services.AddTransient<IUpdater, CacheUpdater>();

        services.RegisterTypes(sources);
        services.RegisterTypes(updaters);

        services.RegisterList<IStaticSource>(sources);
        services.RegisterList<IUpdater>(updaters);

        services.AddTransient<CombinedLibrary>();
        services.AddTransient<CombinedUpdater>();

        services.AddTransient<Func<ILibrary>>(sp => () => sp.GetCombinedLibrary());
        services.AddTransient<Func<IUpdater>>(sp => () => sp.GetCombinedUpdater());

        return services;
    }

    private static ILibrary GetCombinedLibrary(this IServiceProvider sp)
    {
        var cache = sp.GetRequiredService<ICache>();
        var merger = sp.GetRequiredService<IMerger>(); 
        var sources = sp.GetRequiredService<IEnumerable<IStaticSource>>();
        
        var itemCacher = new SimpleCacher(merger, cache);
        var cachedLibrary = new CachedLibrary(cache);
        var cacher = new StaticLibraryCacher(itemCacher);

        return new CombinedLibrary(cacher, cachedLibrary, sources);
    }

    private static IUpdater GetCombinedUpdater(this IServiceProvider sp)
    {
        var updaters = sp.GetRequiredService<IEnumerable<ISourceUpdater>>();
        return new CombinedUpdater(updaters);
    }

    private static void RegisterList<IInterface>(this IServiceCollection services, Type[] types) where IInterface : class
    {
        services.AddTransient(sp => types
            .Select(t => sp.GetService(t) as IInterface));
    }

    private static void RegisterTypes(this IServiceCollection services, Type[] types)
    {
        foreach (var type in types)
        {
            services.AddTransient(type);
        }
    }

    private static void VerifyTypes<IInterface>(Type[] types)
    {
        foreach (var source in types)
        {
            if (!source.IsAssignableTo(typeof(IInterface)))
            {
                throw new InvalidOperationException($"The type {source.Name} does not implement the interface {typeof(IInterface).Name}");
            }
        }
    }
}