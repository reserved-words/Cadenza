global using Cadenza.Common.Domain.Enums;
global using Cadenza.Common.Utilities.Interfaces;
global using Cadenza.Web.Common.Interfaces;
global using Cadenza.Web.Common.Model;
global using Cadenza.Web.LastFM.Interfaces;
global using Cadenza.Web.LastFM.Services;
global using Cadenza.Web.LastFM.Settings;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;

global using Cadenza.State.Store;
global using Cadenza.Web.Common.ViewModels;
global using Fluxor;


namespace Cadenza.Web.LastFM;

public static class _Startup
{
    public static IServiceCollection AddLastFm(this IServiceCollection services)
    {
        return services
            .AddTransient<IPlayTracker, Scrobbler>()
            .AddTransient<IFavouritesService, Favourites>()
            .AddTransient<IAuthoriser, Authoriser>()
            .AddTransient<IPlayHistory, History>()
            .AddTransient<ILastFmHttpHelper, LastFmHttpHelper>();
    }
}