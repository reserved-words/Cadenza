using Cadenza.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.LastFM;

public static class Startup
{
    public static IServiceCollection AddLastFM(this IServiceCollection services)
    {
        return services.AddTransient<ILastFmSigner, LastFmSigner>()
            .AddTransient<ILastFmAuth, LastFmAuth>()
            .AddTransient<ILastFmClient, LastFmClient>()
            .AddTransient<ILastFmAuthorisedClient, LastFmAuthorisedClient>()
            .AddTransient<Scrobbler>()
            .AddTransient<FavouritesController>()
            .AddTransient<FavouritesConsumer>();
    }

    public static IServiceCollection ConfigureLastFM(this IServiceCollection services, IConfiguration config, params string[] sectionPath)
    {
        return services.ConfigureOptions<LastFmSettings>(config, sectionPath);
    }
}