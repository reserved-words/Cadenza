using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.LastFM;

public static class _Startup
{
    public static IServiceCollection AddLastFMCore(this IServiceCollection services)
    {
        return services
            .AddTransient<IAuthoriser, Authoriser>()
            .AddTransient<IHistory, History>();
    }
}