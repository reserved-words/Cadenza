global using Microsoft.Extensions.DependencyInjection;
global using System.Globalization;
global using System.Web;
global using System.Xml.Linq;
global using Cadenza.Common.LastFm.Interfaces;
global using Cadenza.Common.LastFm.Implementations;
global using Cadenza.Common.LastFm.Services;
global using Cadenza.Common.LastFm.Settings;
using Cadenza.API.Interfaces.LastFm;

namespace Cadenza.Common.LastFm;

public static class _Startup
{
    public static IServiceCollection AddLastFm(this IServiceCollection services)
    {
        services.AddInternals();
        services.AddTransient<ILastFmInfoService, LastFmInfoService>();
        services.AddTransient<ILastFmSessionService, LastFmSessionService>();
        services.AddTransient<ILastFmUserService, LastFmUserService>();
        return services;
    }

    private static IServiceCollection AddInternals(this IServiceCollection services)
    {
        return services
            .AddTransient<ISigner, Signer>()
            .AddTransient<IApiClient, ApiClient>()
            .AddTransient<IAuthorisedApiClient, AuthorisedApiClient>()
            .AddTransient<IHasher, Hasher>()
            .AddTransient<IParser, Parser>()
            .AddTransient<IResponseReader, ResponseReader>()
            .AddTransient<IUrlService, UrlService>();
    }
}
