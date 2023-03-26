global using Cadenza.Common.Domain.Model.Album;
global using Cadenza.Common.Domain.Model.Artist;
global using Cadenza.Common.Domain.Model.Track;
global using Cadenza.Common.Domain.Enums;
global using Cadenza.Common.Domain.Model;
global using Cadenza.Common.Interfaces.Utilities;

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
            .AddTransient<IInsertService, InsertService>()
            .AddTransient<IReadService, ReadService>();
    }
}
