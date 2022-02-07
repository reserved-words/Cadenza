using Cadenza.Library;
using Cadenza.Spotify.API;
using Cadenza.Spotify.API.Interfaces;
using Cadenza.Spotify.Libraries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cadenza.Spotify;

public static class Startup
{
    public static IServiceCollection AddSpotify(this IServiceCollection services)
    {
        return services
            .AddTransient<IBuilder, Builder>()
            .AddTransient<IAuthoriser, Authoriser>()
            .AddSingleton<IApiCaller, ApiCaller>()
            .AddSingleton<IApiHelper, ApiHelper>()
            .AddSingleton<IApiToken, ApiToken>()
            .AddLibrary<ApiLibrary, OverridesLibrary>()
            .AddTransient<ISearchRepository, SearchRepository>();
    }

    public static IServiceCollection ConfigureSpotify(this IServiceCollection services, IConfiguration config, string sectionPath)
    {
        var section = config.GetSection(sectionPath);
        return services.Configure<SpotifySettings>(section);
    }
}