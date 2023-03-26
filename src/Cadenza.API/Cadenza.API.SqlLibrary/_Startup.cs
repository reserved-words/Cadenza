using Cadenza.API.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.API.SqlLibrary;

public static class _Startup
{
    public static IServiceCollection AddSqlLibrary(this IServiceCollection services)
    {
        return services
            .AddTransient<IMusicRepository, MusicRepository>()
            .AddTransient<IUpdateRepository, UpdateRepository>();
    }
}
