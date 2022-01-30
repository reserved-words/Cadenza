using Cadenza.API.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.API.Wrapper;

public static class _Startup
{
    public static IServiceCollection AddLastFMCore(this IServiceCollection services)
    {
        return services.AddTransient<Core.LastFM.IAuthoriser, LastFM.Authoriser>();
    }

    public static IServiceCollection AddSpotifyCore(this IServiceCollection services)
    {
        return services.AddTransient<Core.Spotify.IAuthoriser, Spotify.Authoriser>();
    }

    public static IServiceCollection ConfigureCoreAPI(this IServiceCollection services, IConfiguration config, string sectionPath)
    {
        var section = config.GetSection(sectionPath);
        return services.Configure<ApiSettings>(section);
    }

}