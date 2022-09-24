using Cadenza.API.Common.Repositories;
using Cadenza.API.Database.Interfaces;
using Cadenza.API.Database.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FileAccess = Cadenza.API.Database.Services.FileAccess;

namespace Cadenza.API.Database;

public static class _Startup
{
    public static IServiceCollection AddJsonLibrary(this IServiceCollection services)
    {
        return services
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

    public static IServiceCollection ConfigureJsonLibrary(this IServiceCollection services, IConfiguration config, string sectionPath)
    {
        var section = config.GetSection(sectionPath);
        return services.Configure<LibraryPaths>(section);
    }
}
