global using Cadenza.API.Interfaces.Controllers;
global using Cadenza.API.Interfaces.LastFm;
global using Cadenza.API.Interfaces.Repositories;
global using Cadenza.Common.Domain.Enums;
global using Cadenza.Common.Domain.Model;

global using Cadenza.API.Interfaces;
global using Cadenza.Common.DTO;

global using Microsoft.Extensions.DependencyInjection;
using Cadenza.API.Core.Services;

namespace Cadenza.API.Core;

public static class Startup
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        return services
            .AddServices()
            .AddTransient<ICachePopulater, CachePopulater>();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IAdminService, AdminService>()
            .AddTransient<IHistoryService, HistoryService>()
            .AddTransient<IImageService, ImageService>()
            .AddTransient<ILastFmService, LastFmService>()
            .AddTransient<ILibraryService, LibraryService>()
            .AddTransient<IPlayTrackService, PlayTrackService>()
            .AddTransient<ISearchService, SearchService>()
            .AddTransient<IStartupService, StartupService>()
            .AddTransient<ISyncService, SyncService>()
            .AddTransient<IUpdateService, UpdateService>()
            .AddTransient<IWebInfoService, WebInfoService>();
    }
}
