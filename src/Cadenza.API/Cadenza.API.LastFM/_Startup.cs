global using Cadenza.API.Interfaces.LastFm;
global using Cadenza.API.LastFM.Interfaces;
global using Cadenza.API.LastFM.Services;
global using Cadenza.API.LastFM.Settings;
global using Cadenza.Common.DTO;
global using Cadenza.Common.Enums;
global using Cadenza.Common.Http;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using System.Globalization;
global using System.Web;
global using System.Xml.Linq;


namespace Cadenza.API.LastFM;

public static class Startup
{
    public static IServiceCollection AddLastFM(this IServiceCollection services)
    {
        return services
            .AddInternals()
            .AddTransient<IAuthoriser, Authoriser>()
            .AddTransient<IHistory, History>()
            .AddTransient<IInfoService, InfoService>();
    }

    private static IServiceCollection AddInternals(this IServiceCollection services)
    {
        return services
            .AddTransient<ISigner, Signer>()
            .AddTransient<IApiClient, ApiClient>()
            .AddTransient<IHasher, Hasher>()
            .AddTransient<IParser, Parser>()
            .AddTransient<IResponseReader, ResponseReader>()
            .AddTransient<IUrlService, UrlService>();
    }
}