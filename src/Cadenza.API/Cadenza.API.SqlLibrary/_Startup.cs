﻿global using Cadenza.Database.Interfaces;
global using Cadenza.Database.SqlLibrary.Interfaces;
global using Cadenza.Common.DTO;
global using Cadenza.Common.Enums;
global using Cadenza.Common.Model;
global using Cadenza.Common.Utilities.Interfaces;
global using Cadenza.Database.SqlLibrary.Model;

using Cadenza.Database.SqlLibrary.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Database.SqlLibrary;

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
            .AddTransient<IPlayTrackRepository, PlayTrackRepository>()
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
