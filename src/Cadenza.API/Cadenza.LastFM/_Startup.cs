using Cadenza.API.Core.LastFM;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.LastFM;

public static class Startup
{
    public static IServiceCollection AddLastFM(this IServiceCollection services)
    {
        return services.AddTransient<ILastFmSigner, LastFmSigner>()
            .AddTransient<ILastFmClient, LastFmClient>()
            .AddTransient<ILastFmAuthorisedClient, LastFmAuthorisedClient>()
            .AddTransient<IAuthoriser, LastFmAuth>()
            .AddTransient<IFavourites, Favourites>()
            .AddTransient<IHistory, History>()
            .AddTransient<IScrobbler, Scrobbler>();
    }

    public static IServiceCollection ConfigureLastFM(this IServiceCollection services, IConfiguration config, string sectionPath)
    {
        var section = config.GetSection(sectionPath);
        return services.Configure<LastFmSettings>(section);
    }
}