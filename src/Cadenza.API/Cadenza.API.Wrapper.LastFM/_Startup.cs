using Cadenza.API.Core.LastFM;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.API.Wrapper.LastFM;

public static class _Startup
{
    public static IServiceCollection AddLastFMCore(this IServiceCollection services)
    {
        return services
            .AddTransient<IAuthoriser, Authoriser>()
            .AddTransient<IFavourites, Favourites>()
            .AddTransient<IHistory, History>()
            .AddTransient<IScrobbler, Scrobbler>();
    }
}