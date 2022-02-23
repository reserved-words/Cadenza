using Cadenza.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.LastFM;

public static class _Startup
{
    public static IServiceCollection AddLastFm(this IServiceCollection services, string apiSectionName)
    {
        services.AddOptions<LastFmApiSettings>(apiSectionName);

        return services
            .AddTransient<IPlayTracker, Scrobbler>()
            .AddTransient<IFavouritesConsumer, Favourites>()
            .AddTransient<IFavouritesController, Favourites>()
            .AddTransient<IAuthoriser, Authoriser>()
            .AddTransient<IHistory, History>()
            .AddTransient<IConnectionTaskBuilder, LastFmConnectionTaskBuilder>();
    }
}