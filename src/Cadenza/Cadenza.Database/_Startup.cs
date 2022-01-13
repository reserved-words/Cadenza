global using Cadenza.Common;
global using IndexedDB.Blazor;
global using Newtonsoft.Json;
global using System.ComponentModel.DataAnnotations;

using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Database;

public static class Startup
{
    public static IServiceCollection AddDatabaseRepositories(this IServiceCollection services)
    {
        return services
            .AddSingleton<IIndexedDbFactory, IndexedDbFactory>()
            .AddTransient<IMainRepository, MainRepository>()
            .AddTransient<IArtistRepository, ArtistRepository>()
            .AddTransient<IAlbumRepository, AlbumRepository>()
            .AddTransient<ITrackRepositoryUpdater, TrackRepository>()
            .AddTransient<IPlayTrackRepositoryUpdater, PlayTrackRepository>();
    }
}

