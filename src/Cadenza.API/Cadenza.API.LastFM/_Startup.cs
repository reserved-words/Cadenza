global using Cadenza.API.Interfaces.LastFm;
global using Cadenza.API.LastFM.Interfaces;
global using Cadenza.API.LastFM.Services;
global using Cadenza.API.LastFM.Settings;
global using Cadenza.Common.Domain.Enums;
global using Cadenza.Common.Domain.Model.History;
global using Cadenza.Common.Domain.Model.LastFm;
global using Cadenza.Common.Utilities.Interfaces;
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
            .AddTransient<IFavourites, Favourites>()
            .AddTransient<IHistory, History>()
            .AddTransient<IInfoService, InfoService>()
            .AddTransient<IScrobbler, Scrobbler>();
    }

    private static IServiceCollection AddInternals(this IServiceCollection services)
    {
        return services
            .AddTransient<ISigner, Signer>()
            .AddTransient<IApiClient, ApiClient>()
            .AddTransient<IAuthorisedApiClient, AuthorisedApiClient>()
            .AddTransient<IParser, Parser>()
            .AddTransient<IResponseReader, ResponseReader>()
            .AddTransient<IUrlService, UrlService>();
    }
}