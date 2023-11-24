﻿global using Cadenza.Database.Interfaces;
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
using Cadenza.Database.SqlLibrary.Mappers;

namespace Cadenza.Database.SqlLibrary;

public static class _Startup
{
    public static IServiceCollection AddSqlLibrary(this IServiceCollection services)
    {
        return services
            .AddSql()
            .AddMappers()
            .AddTransient<IAdminRepository, AdminRepository>()
            .AddTransient<IHistoryRepository, HistoryRepository>()
            .AddTransient<IImageRepository, ImageRepository>()
            .AddTransient<ILibraryRepository, LibraryRepository>()
            .AddTransient<IPlayRepository, PlayRepository>()
            .AddTransient<IQueueRepository, QueueRepository>()
            .AddTransient<IUpdateRepository, UpdateRepository>();
    }

    private static IServiceCollection AddSql(this IServiceCollection services)
    {
        return services
            .AddTransient<ISqlAccessFactory, SqlAccessFactory>()
            .AddTransient<IAdmin, Admin>()
            .AddTransient<IHistory, History>()
            .AddTransient<IImages, Images>()
            .AddTransient<ILibrary, Library>()
            .AddTransient<IPlay, Play>()
            .AddTransient<IQueue, Queue>()
            .AddTransient<IUpdate, Update>();
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        return services
            .AddTransient<IAdminMapper, AdminMapper>()
            .AddTransient<IHistoryMapper, HistoryMapper>()
            .AddTransient<ILibraryMapper, LibraryMapper>()
            .AddTransient<IQueueMapper, QueueMapper>()
            .AddTransient<IUpdateMapper, UpdateMapper>();
    }
}
