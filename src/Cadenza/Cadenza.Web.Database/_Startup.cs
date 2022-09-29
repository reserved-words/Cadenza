global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using System.Net.Http.Json;

global using Cadenza.Domain.Enums;
global using Cadenza.Domain.Model;
global using Cadenza.Domain.Model.Album;
global using Cadenza.Domain.Model.Artist;
global using Cadenza.Domain.Model.Track;
global using Cadenza.Domain.Model.Update;
global using Cadenza.Domain.Model.Updates;

global using Cadenza.Library.Repositories;

global using Cadenza.Utilities.Interfaces;

global using Cadenza.Web.Common.Enums;
global using Cadenza.Web.Common.Interfaces;
global using Cadenza.Web.Common.Model;
global using Cadenza.Web.Common.Tasks;

global using Cadenza.Web.Database.Interfaces;
global using Cadenza.Web.Database.Repositories;
global using Cadenza.Web.Database.Settings;
global using Cadenza.Web.Database.Services;

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
        return services
            .AddTransient<IArtistRepository, ArtistRepository>()
            .AddTransient<IAlbumRepository, AlbumRepository>()
            .AddTransient<IPlayTrackRepository, PlayTrackRepository>()
            .AddTransient<ISearchRepository, SearchRepository>()
            .AddTransient<ITrackRepository, TrackRepository>()
            .AddTransient<IUpdateService, UpdateService>();
    }

    private static IServiceCollection AddInternals(this IServiceCollection services)
    {
        return services
            .AddTransient<IApiHelper, ApiHelper>();
    }
}
