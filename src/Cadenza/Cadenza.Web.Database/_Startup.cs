global using Cadenza.Common.DTO;
global using Cadenza.Common.Enums;
global using Cadenza.Web.Common.Interfaces;
global using Cadenza.Web.Common.Model;
global using Cadenza.Web.Database.Interfaces;
global using Cadenza.Web.Database.Repositories;
global using Cadenza.Web.Database.Services;
global using Cadenza.Web.Database.Settings;
global using Cadenza.Web.Model;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;

namespace Cadenza.Web.Database;

public static class Startup
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        return services
            .AddMappers()
            .AddApiRepositories()
            .AddTransient<IWebInfoService, WebInfoService>()
            .AddTransient<IApiHttpHelper, ApiHttpHelper>()
            .AddTransient<IApiConnector, ApiConnector>();
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        return services
            .AddTransient<IViewModelMapper, ViewModelMapper>()
            .AddTransient<IDataTransferObjectMapper, DataTransferObjectMapper>();
    }

    private static IServiceCollection AddApiRepositories(this IServiceCollection services)
    {
        services
            .AddTransient<IArtworkFetcher, ArtworkFetcher>();

        return services
            .AddTransient<IAdminRepository, AdminRepository>()
            .AddTransient<IAlbumRepository, AlbumRepository>()
            .AddTransient<IArtistRepository, ArtistRepository>()
            .AddTransient<IPlayTrackRepository, PlayTrackRepository>()
            .AddTransient<ISearchRepository, SearchRepository>()
            .AddTransient<ITagRepository, TagRepository>()
            .AddTransient<ITrackRepository, TrackRepository>()
            .AddTransient<IUpdateRepository, UpdateRepository>()
            .AddTransient<IHistoryRepository, HistoryRepository>()
            .AddTransient<IPlayTracker, HistoryRepository>()
;
    }
}
