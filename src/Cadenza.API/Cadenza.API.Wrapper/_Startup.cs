using Cadenza.API.Core;
using Cadenza.API.Core.Spotify;
using Cadenza.API.Wrapper.Spotify;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.API.Wrapper;

public static class _Startup
{
    public static IServiceCollection AddSpotifyCore(this IServiceCollection services)
    {
        return services.AddTransient<IAuthoriser, Authoriser>();
    }

    public static IServiceCollection ConfigureCoreAPI(this IServiceCollection services, IConfiguration config, string sectionPath)
    {
        var section = config.GetSection(sectionPath);
        return services.Configure<ApiSettings>(section);
    }

}