global using Cadenza.API.Interfaces.Repositories;
global using Cadenza.Common.Domain.Enums;
global using Cadenza.Common.Domain.Model;
global using Cadenza.Common.Domain.Model.Album;
global using Cadenza.Common.Domain.Model.Artist;
global using Cadenza.Common.Domain.Model.Track;
global using Cadenza.Common.Domain.Model.Updates;
global using Cadenza.Common.Interfaces.Utilities;
global using Microsoft.Extensions.Options;
using Cadenza.API.JsonLibrary.Interfaces;
using Cadenza.API.JsonLibrary.Interfaces.Updaters;
using Cadenza.API.JsonLibrary.Services;
using Cadenza.API.JsonLibrary.Services.Updaters;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.API.JsonLibrary;

public static class _Startup
{
    public static IServiceCollection AddJsonLibrary(this IServiceCollection services)
    {
        return services
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
            .AddTransient<IFilePathService, FilePathService>();
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
}
