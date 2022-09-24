using Cadenza.API.Common.Repositories;
using Cadenza.API.Database.Interfaces;
using Cadenza.API.Database.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FileAccess = Cadenza.API.Database.Services.FileAccess;

namespace Cadenza.API.Database;

public static class _Startup
{
    public static IServiceCollection AddJsonLibrary(this IServiceCollection services, IConfigurationSection libraryPathsConfig)
    {
        return services
            .Configure<LibraryPaths>(libraryPathsConfig)
            .AddInternalServices()
            .AddTransient<IMusicRepository, MusicRepository>();
    }

    private static IServiceCollection AddInternalServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IAlbumConverter, AlbumConverter>()
            .AddTransient<IAlbumTrackLinkConverter, AlbumTrackLinkConverter>()
            .AddTransient<IArtistConverter, ArtistConverter>()
            .AddTransient<IDataAccess, DataAccess>()
            .AddTransient<IFileAccess, FileAccess>()
            .AddTransient<IJsonToModelConverter, JsonToModelConverter>()
            .AddTransient<ITrackConverter, TrackConverter>();
    }
}
