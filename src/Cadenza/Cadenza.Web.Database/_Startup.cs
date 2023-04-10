global using Cadenza.Common.Domain.Enums;
global using Cadenza.Common.Domain.Model;
global using Cadenza.Common.Domain.Model.Album;
global using Cadenza.Common.Domain.Model.Artist;
global using Cadenza.Common.Domain.Model.Track;
global using Cadenza.Common.Domain.Model.Update;
global using Cadenza.Common.Domain.Model.Updates;
global using Cadenza.Common.Interfaces.Repositories;
global using Cadenza.Common.Interfaces.Utilities;
global using Cadenza.Web.Common.Enums;
global using Cadenza.Web.Common.Interfaces;
global using Cadenza.Web.Common.Model;
global using Cadenza.Web.Common.Tasks;
global using Cadenza.Web.Database.Interfaces;
global using Cadenza.Web.Database.Repositories;
global using Cadenza.Web.Database.Services;
global using Cadenza.Web.Database.Settings;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using System.Net.Http.Json;
using Cadenza.Web.Common.Interfaces.Startup;
using Cadenza.Web.Common.Interfaces.Updates;

namespace Cadenza.Web.Database;

public static class Startup
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        return services
            .AddInternals()
            .AddApiRepositories()
            .AddTransient<IConnector, DatabaseConnector>();
    }

    private static IServiceCollection AddApiRepositories(this IServiceCollection services)
    {
        services
            .AddSingleton<IArtworkFetcher, ArtworkFetcher>();

        return services
            .AddTransient<IAlbumRepository, AlbumRepository>()
            .AddTransient<IArtistRepository, ArtistRepository>()
            .AddTransient<IPlayTrackRepository, PlayTrackRepository>()
            .AddTransient<ISearchRepository, SearchRepository>()
            .AddTransient<ITagRepository, TagRepository>()
            .AddTransient<ITrackRepository, TrackRepository>()
            .AddTransient<IUpdateService, UpdateService>()
            .AddTransient<IHistoryLogger, HistoryLogger>();
    }

    private static IServiceCollection AddInternals(this IServiceCollection services)
    {
        return services
            .AddTransient<IApiHelper, ApiHelper>();
    }
}
