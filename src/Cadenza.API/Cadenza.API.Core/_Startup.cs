global using Cadenza.API.Core.Interfaces;
global using Cadenza.API.Core.Interfaces.Cache;
global using Cadenza.API.Core.Services;
global using Cadenza.API.Core.Services.Cache;

global using Cadenza.API.Interfaces.Controllers;
global using Cadenza.API.Interfaces.Repositories;

global using Cadenza.Domain.Model;
global using Cadenza.Domain.Model.Album;
global using Cadenza.Domain.Model.Artist;
global using Cadenza.Domain.Model.History;
global using Cadenza.Domain.Model.Track;
global using Cadenza.Domain.Model.Update;
global using Cadenza.Domain.Model.Updates;

global using Cadenza.Domain.Enums;
global using Cadenza.Domain.Extensions;

global using Microsoft.Extensions.DependencyInjection;

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
            .AddTransient<IStartupService, StartupService>()
            .AddTransient<ISyncService, SyncService>()
            .AddTransient<IUpdateService, UpdateService>();
    }
}
