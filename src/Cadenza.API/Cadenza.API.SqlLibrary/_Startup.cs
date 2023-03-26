using Cadenza.API.Interfaces.Repositories;
using Cadenza.API.SqlLibrary.Interfaces;
using Cadenza.API.SqlLibrary.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.API.SqlLibrary;

public static class _Startup
{
    public static IServiceCollection AddSqlLibrary(this IServiceCollection services)
    {
        return services
            .AddInternalServices()
            .AddTransient<IMusicRepository, MusicRepository>()
            .AddTransient<IUpdateRepository, UpdateRepository>();
    }

    private static IServiceCollection AddInternalServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IDatabaseAccess, DatabaseAccess>()
            .AddTransient<IDataMapper, DataMapper>()
            .AddTransient<IInsertService, InsertService>();
    }
}
