global using Cadenza.Domain;
global using Cadenza.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Library;

public static class _Startup
{
    public static IServiceCollection AddCombinedLibrary<T>(this IServiceCollection services) 
        where T : class, IStaticSource
    {
        return services.AddCachedLibrary(typeof(T));
    }

    public static IServiceCollection AddCombinedLibrary<T1, T2>(this IServiceCollection services)
        where T1 : class, IStaticSource
        where T2 : class, IStaticSource
    {
        return services
            .AddCachedLibrary(typeof(T1), typeof(T2));
    }

    private static IServiceCollection AddCachedLibrary(this IServiceCollection services, params Type[] types)
    {
        services.AddSingleton<ICache, Cache>();
        services.AddTransient<IMerger, Merger>();
        services.AddTransient<ILibrary, CombinedLibrary>();
        services.AddTransient<ILibraryUpdater, CacheUpdater>();

        foreach (var type in types)
        {
            services.AddTransient(type);
        }

        return services.AddTransient(sp => types.Select(t => sp.GetService(t)).ToList());
    }
}