global using Cadenza.API.Interfaces.Repositories;
global using Cadenza.Common.Domain.Enums;
global using Cadenza.Common.Domain.Model;
global using Cadenza.Common.Domain.Model.Album;
global using Cadenza.Common.Domain.Model.Artist;
global using Cadenza.Common.Domain.Model.Track;
global using Cadenza.Common.Interfaces.Repositories;
global using Microsoft.Extensions.DependencyInjection;
global using Cadenza.API.Cache.Interfaces;
global using Cadenza.API.Cache.Services;
global using Cadenza.API.Cache.Services.Cache;
using Cadenza.API.Interfaces;

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
            .AddTransient<ICachePopulater, CachePopulater>()
            .AddSingleton<ICacheService, CacheService>();
    }
}
