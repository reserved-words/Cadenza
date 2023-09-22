﻿global using Cadenza.Common.Domain.Enums;
global using Cadenza.Common.Domain.Model.History;
global using Cadenza.Common.Domain.Model.LastFm;
global using Cadenza.Common.Domain.Model.Track;
global using Cadenza.Common.Interfaces.Utilities;
global using Cadenza.Web.Common.Enums;
global using Cadenza.Web.Common.Interfaces;
global using Cadenza.Web.Common.Model;
global using Cadenza.Web.Common.Tasks;
global using Cadenza.Web.LastFM.Interfaces;
global using Cadenza.Web.LastFM.Services;
global using Cadenza.Web.LastFM.Settings;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;

namespace Cadenza.Web.LastFM;

public static class _Startup
{
    public static IServiceCollection AddLastFm(this IServiceCollection services)
    {
        return services
            .AddTransient<IPlayTracker, Scrobbler>()
            .AddTransient<IFavouritesMessenger, Favourites>()
            .AddTransient<IFavouritesController, Favourites>()
            .AddTransient<IAuthoriser, Authoriser>()
            .AddTransient<IHistory, History>()
            .AddTransient<IConnector, LastFmConnector>()
            .AddTransient<ILastFmHttpHelper, LastFmHttpHelper>();
    }
}