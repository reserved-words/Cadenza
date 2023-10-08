global using Cadenza.Common.Enums;
global using Cadenza.Common.Model;
global using Cadenza.Common.Utilities.Interfaces;
global using Cadenza.API.Interfaces.Repositories;
global using Cadenza.API.SqlLibrary.Interfaces;
global using Cadenza.Common.DTO;

using Cadenza.API.SqlLibrary.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.API.SqlLibrary;

public static class _Startup
{
    public static IServiceCollection AddSqlLibrary(this IServiceCollection services)
    {
        return services
            .AddInternalServices()
            .AddTransient<IAdminRepository, AdminRepository>()
            .AddTransient<IHistoryRepository, HistoryRepository>()
            .AddTransient<IImageRepository, ImageRepository>()
            .AddTransient<IMusicRepository, MusicRepository>()
            .AddTransient<IUpdateRepository, UpdateRepository>();
    }

    private static IServiceCollection AddInternalServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IDataAccess, DataAccess>()
            .AddTransient<IDataDeletionService, DataDeletionService>()
            .AddTransient<IDataInsertService, DataInsertService>()
            .AddTransient<IDataMapper, DataMapper>()
            .AddTransient<IDataReadService, DataReadService>()
            .AddTransient<IDataUpdateService, DataUpdateService>()
            .AddTransient<ILibraryReader, LibraryReader>()
            .AddTransient<ILibraryUpdater, LibraryUpdater>()
            .AddTransient<IQueueReader, QueueReader>()
            .AddTransient<IQueueUpdater, QueueUpdater>()
            .AddTransient<ITrackAdder, TrackAdder>()
            .AddTransient<ITrackRemover, TrackRemover>();
    }
}
