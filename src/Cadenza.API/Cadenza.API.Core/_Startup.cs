global using Cadenza.API.Core.Interfaces;
global using Cadenza.API.Core.Interfaces.Cache;
global using Cadenza.API.Core.Services;
global using Cadenza.API.Core.Services.Cache;

global using Cadenza.API.Interfaces.Controllers;
global using Cadenza.API.Interfaces.Repositories;
global using Cadenza.API.LastFM;
global using Cadenza.API.LastFM.Interfaces;

global using Cadenza.Domain.Models;
global using Cadenza.Domain.Models.Album;
global using Cadenza.Domain.Models.History;
global using Cadenza.Domain.Models.Track;
global using Cadenza.Domain.Models.Update;

global using Cadenza.Domain.Enums;
global using Cadenza.Domain.Extensions;
global using Cadenza.Domain.Models.Artist;
global using Cadenza.Domain.Models.Updates;

global using Cadenza.Library;

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
