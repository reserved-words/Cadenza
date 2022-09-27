global using Cadenza.API.Common.Model;
global using Cadenza.API.Common.Repositories;
global using Cadenza.API.Database.Interfaces;
global using Cadenza.API.Database.Interfaces.Converters;
global using Cadenza.API.Database.Interfaces.Updaters;
global using Cadenza.API.Database.Model;
global using Cadenza.API.Database.Services;
global using Cadenza.API.Database.Services.Converters;
global using Cadenza.API.Database.Services.Updaters;

global using Cadenza.Domain.Enums;
global using Cadenza.Domain.Extensions;
global using Cadenza.Domain.Models.Album;
global using Cadenza.Domain.Models.Artist;
global using Cadenza.Domain.Models.Track;
global using Cadenza.Domain.Models.Updates;

global using Cadenza.Utilities.Interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.API.Database;

public static class _Startup
{
    public static IServiceCollection AddJsonLibrary(this IServiceCollection services)
    {
        return services
            .AddConverters()
            .AddDataAccess()
            .AddUpdaters()
            .AddUtilities()
            .AddTransient<IMusicRepository, MusicRepository>()
            .AddTransient<IUpdateRepository, UpdateRepository>();
    }

    private static IServiceCollection AddUtilities(this IServiceCollection services)
    {
        return services
            .AddTransient<IFileDataService, FileDataService>()
            .AddTransient<IFilePathService, FilePathService>()
            .AddTransient<IJsonToModelConverter, JsonToModelConverter>()
            .AddTransient<IModelToJsonConverter, ModelToJsonConverter>();
    }

    private static IServiceCollection AddConverters(this IServiceCollection services)
    {
        return services
            .AddTransient<IAlbumConverter, AlbumConverter>()
            .AddTransient<IAlbumTrackConverter, AlbumTrackConverter>()
            .AddTransient<IArtistConverter, ArtistConverter>()
            .AddTransient<ITrackConverter, TrackConverter>();
    }

    private static IServiceCollection AddUpdaters(this IServiceCollection services)
    {
        return services
            .AddTransient<IItemUpdater, ItemUpdater>()
            .AddTransient<ILibraryUpdater, LibraryUpdater>()
            .AddTransient<IQueueUpdater, QueueUpdater>();
    }

    private static IServiceCollection AddDataAccess(this IServiceCollection services)
    {
        return services
            .AddTransient<DataAccess>()
            .AddTransient<IDataAccess>(sp => new ThreadSafeDataAccess(sp.GetRequiredService<DataAccess>()));
    }

    public static IServiceCollection ConfigureJsonLibrary(this IServiceCollection services, IConfiguration config, string sectionPath)
    {
        var section = config.GetSection(sectionPath);
        return services.Configure<LibraryPathSettings>(section);
    }
}
