using Cadenza.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.LastFM;

public static class _Startup
{
    public static IServiceCollection AddLastFm(this IServiceCollection services, IConfiguration config, string apiSectionName)
    {
        services.Configure<LastFmApiSettings>(config.GetSection(apiSectionName));

        return services
            .AddTransient<IPlayTracker, Scrobbler>()
            .AddTransient<IFavouritesConsumer, Favourites>()
            .AddTransient<IFavouritesController, Favourites>()
            .AddTransient<IAuthoriser, Authoriser>()
            .AddTransient<IHistory, History>()
            .AddTransient<IConnectionTaskBuilder, LastFmConnectionTaskBuilder>();
    }
}