global using Cadenza.Common.Domain.Enums;
global using Cadenza.Common.DTO;
global using Cadenza.Common.Utilities.Interfaces;
global using Cadenza.Web.Common.Interfaces;
global using Cadenza.Web.Common.Model;
global using Cadenza.Web.Database.Interfaces;
global using Cadenza.Web.Database.Repositories;
global using Cadenza.Web.Database.Services;
global using Cadenza.Web.Database.Settings;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using Cadenza.Web.Common.Interfaces.Library;
global using Cadenza.Web.Model;

namespace Cadenza.Web.Database;

public static class Startup
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        return services
            .AddMappers()
            .AddApiRepositories()
            .AddTransient<IApiHttpHelper, ApiHttpHelper>();
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
            .AddTransient<IPlaylistHistory, HistoryRepository>();
    }
}
