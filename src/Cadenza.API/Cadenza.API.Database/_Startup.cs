using Cadenza.API.Common.Repositories;
using Cadenza.API.Database.Converters;
using Cadenza.API.Database.Interfaces;
using Cadenza.API.Database.Interfaces.Converters;
using Cadenza.API.Database.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.API.Database;

public static class _Startup
{
    public static IServiceCollection AddJsonLibrary(this IServiceCollection services)
    {
        return services
            .AddInternalServices()
            .AddTransient<IMusicRepository, MusicRepository>()
            .AddTransient<IUpdateRepository, UpdateRepository>();
    }

    private static IServiceCollection AddInternalServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IAlbumConverter, AlbumConverter>()
            .AddTransient<IAlbumTrackLinkConverter, AlbumTrackLinkConverter>()
            .AddTransient<IArtistConverter, ArtistConverter>()
            .AddTransient<IDataAccess, DataAccess>()
            .AddTransient<IJsonToModelConverter, JsonToModelConverter>()
            .AddTransient<IModelToJsonConverter, ModelToJsonConverter>()
            .AddTransient<ITrackConverter, TrackConverter>();
    }

    public static IServiceCollection ConfigureJsonLibrary(this IServiceCollection services, IConfiguration config, string sectionPath)
    {
        var section = config.GetSection(sectionPath);
        return services.Configure<LibraryPathSettings>(section);
    }
}
