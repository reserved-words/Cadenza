global using Cadenza.Common.Enums;
global using Cadenza.Web.Common.Interfaces;
global using Cadenza.Web.Common.Model;
global using Cadenza.Web.LastFM.Interfaces;
global using Cadenza.Web.LastFM.Services;
global using Cadenza.Web.LastFM.Settings;
global using Cadenza.Web.Model;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;


namespace Cadenza.Web.LastFM;

public static class _Startup
{
    public static IServiceCollection AddLastFm(this IServiceCollection services)
    {
        return services
            .AddTransient<IAuthoriser, Authoriser>()
            .AddTransient<ILastFmHttpHelper, LastFmHttpHelper>();
    }
}