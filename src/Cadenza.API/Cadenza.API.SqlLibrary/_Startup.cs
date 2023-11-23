global using Cadenza.Database.Interfaces;
global using Cadenza.Database.SqlLibrary.Interfaces;
global using Cadenza.Common.DTO;
global using Cadenza.Common.Enums;
global using Cadenza.Common.Model;
global using Cadenza.Common.Utilities.Interfaces;
global using Cadenza.Database.SqlLibrary.Model;

using Cadenza.Database.SqlLibrary.Services;
using Microsoft.Extensions.DependencyInjection;
using Cadenza.Database.SqlLibrary.Database;
using Cadenza.Database.SqlLibrary.Repositories;

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
            .AddTransient<IQueueRepository, QueueRepository>();
    }

    private static IServiceCollection AddInternalServices(this IServiceCollection services)
    {
        return services
            .AddTransient<ISqlAccessFactory, SqlAccessFactory>()
            .AddTransient<IDataMapper, DataMapper>()
            .AddTransient<IAdmin, Admin>()
            .AddTransient<IHistory, History>()
            .AddTransient<ILibrary, Library>()
            .AddTransient<IPlay, Play>()
            .AddTransient<IQueue, Queue>()
            .AddTransient<IUpdate, Update>();
    }
}
