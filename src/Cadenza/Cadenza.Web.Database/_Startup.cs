global using Cadenza.Library;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Cadenza.Web.Common.Interfaces;
using Cadenza.Web.Database.Settings;
using Cadenza.Web.Database.Services;
using Cadenza.Utilities.Interfaces;

namespace Cadenza.Web.Database;

public static class Startup
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config, string apiSectionName)
    {
        services.Configure<DatabaseApiSettings>(config.GetSection(apiSectionName));

        return services
            .AddApiRepositories<DatabaseApiRepositorySettings>()
            .AddTransient<IConnectionTaskBuilder, DatabaseConnectionTaskBuilder>()
            .AddTransient<IFileUpdateQueue, UpdateQueueService>();
    }

    public static IServiceCollection AddApiRepositories<T>(this IServiceCollection services) where T : class, IApiRepositorySettings
    {
        return services
            .AddTransient<T>()
            .AddTransient<IArtistRepository>(sp => GetApiRepository<T>(sp))
            .AddTransient<IAlbumRepository>(sp => GetApiRepository<T>(sp))
            .AddTransient<IPlayTrackRepository>(sp => GetApiRepository<T>(sp))
            .AddTransient<ISearchRepository>(sp => GetApiRepository<T>(sp))
            .AddTransient<ITrackRepository>(sp => GetApiRepository<T>(sp));
    }

    private static ApiRepository GetApiRepository<T>(IServiceProvider sp) where T : class, IApiRepositorySettings
    {
        return new ApiRepository(sp.GetService<IHttpHelper>(), sp.GetService<T>());
    }
}
