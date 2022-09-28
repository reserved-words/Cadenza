using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Cadenza.Web.Common.Interfaces;
using Cadenza.Web.Database.Settings;
using Cadenza.Web.Database.Services;
using Cadenza.Library.Repositories;

namespace Cadenza.Web.Database;

public static class Startup
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config, string apiSectionName)
    {
        services.Configure<DatabaseApiSettings>(config.GetSection(apiSectionName));

        return services
            .AddApiRepositories()
            .AddTransient<IConnectionTaskBuilder, DatabaseConnectionTaskBuilder>()
            .AddTransient<IUpdateQueue, UpdateQueueService>();
    }

    public static IServiceCollection AddApiRepositories(this IServiceCollection services) 
    {
        return services
            .AddTransient<IArtistRepository, ApiRepository>()
            .AddTransient<IAlbumRepository, ApiRepository>()
            .AddTransient<IPlayTrackRepository, ApiRepository>()
            .AddTransient<ISearchRepository, ApiRepository>()
            .AddTransient<ITrackRepository, ApiRepository>()
            .AddTransient<IUpdateService, UpdateService>();
    }
}
