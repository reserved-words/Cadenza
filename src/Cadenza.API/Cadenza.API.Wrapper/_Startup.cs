using Cadenza.API.Core;
using Cadenza.API.Wrapper.LastFM;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.API.Wrapper;

public static class _Startup
{
    public static IServiceCollection AddLastFMCore(this IServiceCollection services)
    {
        return services
            .AddCoreServices()
            .AddTransient<IAuthoriser, Authoriser>()
            .AddTransient<Core.LastFM.IFavourites, Favourites>()
            .AddTransient<Core.LastFM.IHistory, History>()
            .AddTransient<Core.LastFM.IScrobbler, Scrobbler>();
    }

    public static IServiceCollection AddSpotifyCore(this IServiceCollection services)
    {
        return services
            .AddCoreServices()
            .AddTransient<Spotify.IAuthoriser, Spotify.Authoriser>();
    }

    public static IServiceCollection ConfigureCoreAPI(this IServiceCollection services, IConfiguration config, string sectionPath)
    {
        var section = config.GetSection(sectionPath);
        return services.Configure<ApiSettings>(section);
    }

    private static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IUrl, Url>()
            .AddTransient<IConnectionChecker, ConnectionChecker>();
    }
}