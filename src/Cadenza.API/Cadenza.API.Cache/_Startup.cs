global using Cadenza.API.Cache.Extensions;
global using Cadenza.API.Cache.Interfaces;
global using Cadenza.API.Cache.Services;
global using Cadenza.API.Interfaces;
global using Cadenza.API.Interfaces.Library;
global using Cadenza.Common.DTO;
global using Cadenza.Common.Enums;
global using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.API.Cache;

public static class Startup
{
    public static IServiceCollection AddCache(this IServiceCollection services)
    {
        return services
            .AddInternalServices()
            .AddSingleton<ILibraryCache, LibraryCache>();
    }

    private static IServiceCollection AddInternalServices(this IServiceCollection services)
    {
        return services
            .AddSingleton<ICacheService, CacheService>()
            .AddSingleton<IHelperCache, HelperCache>()
            .AddSingleton<IItemCache, ItemCache>()
            .AddSingleton<IMainCache, MainCache>()
            .AddSingleton<IPlayCache, PlayCache>();
    }
}
