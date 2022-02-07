using Cadenza.API.Core.LastFM;
using Cadenza.API.Wrapper.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.API.Wrapper.LastFM;

public static class _Startup
{
    public static IServiceCollection AddLastFMCore(this IServiceCollection services)
    {
        return services
            .AddCoreServices()
            .AddTransient<IAuthoriser, Authoriser>()
            .AddTransient<IFavourites, Favourites>()
            .AddTransient<IHistory, History>()
            .AddTransient<IScrobbler, Scrobbler>();
    }
}