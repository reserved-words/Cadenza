global using Cadenza.API.Core.Interfaces;
global using Cadenza.API.Core.Interfaces.Cache;
global using Cadenza.Domain;
global using Cadenza.Library;

using Cadenza.API.Common.Controllers;
using Cadenza.API.Core.Services;
using Cadenza.API.Core.Services.Cache;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.API.Core;

public static class _Startup
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        return services
            .AddCache()
            .AddServices();
    }

    private static IServiceCollection AddCache(this IServiceCollection services)
    {
        return services
            .AddTransient<ICachePopulater, CachePopulater>()
            .AddSingleton<ILibraryCache, LibraryCache>()
            .AddSingleton<IAlbumCache, AlbumCache>()
            .AddSingleton<IArtistCache, ArtistCache>()
            .AddSingleton<IPlayTrackCache, PlayTrackCache>()
            .AddSingleton<ISearchCache, SearchCache>()
            .AddSingleton<ITrackCache, TrackCache>();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddTransient<ILastFmService, LastFmService>()
            .AddTransient<ILibraryService, LibraryService>()
            .AddTransient<IPlayTrackService, PlayTrackService>()
            .AddTransient<ISearchService, SearchService>()
            .AddTransient<IStartupService, StartupService>();
    }
}
