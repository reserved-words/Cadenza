global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using System.Net.Http.Json;

global using Cadenza.Domain.Enums;
global using Cadenza.Domain.Model.History;
global using Cadenza.Domain.Model.LastFm;
global using Cadenza.Domain.Model.Track;

global using Cadenza.Utilities.Extensions;
global using Cadenza.Utilities.Interfaces;

global using Cadenza.Web.Common.Enums;
global using Cadenza.Web.Common.Interfaces;
global using Cadenza.Web.Common.Interop;
global using Cadenza.Web.Common.Model;
global using Cadenza.Web.Common.Tasks;

global using Cadenza.Web.LastFM.Interfaces;
global using Cadenza.Web.LastFM.Services;
global using Cadenza.Web.LastFM.Settings;

global using Track = Cadenza.Domain.Model.LastFm.Track;

namespace Cadenza.Web.LastFM;

public static class _Startup
{
    public static IServiceCollection AddLastFm(this IServiceCollection services)
    {
        return services
            .AddTransient<IPlayTracker, Scrobbler>()
            .AddTransient<IFavouritesConsumer, Favourites>()
            .AddTransient<IFavouritesController, Favourites>()
            .AddTransient<IAuthoriser, Authoriser>()
            .AddTransient<IHistory, History>()
            .AddTransient<IConnector, LastFmConnector>();
    }
}