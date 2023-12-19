global using Cadenza.Common.DTO;
global using Cadenza.Common.Enums;
global using Cadenza.Web.Common.Model;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using Cadenza.Web.Api.Interfaces;
global using Cadenza.Web.Api.Settings;
global using Cadenza.Web.Common.ViewModel;

using Cadenza.Web.Api.Helpers;
using Cadenza.Web.Api.Services;

namespace Cadenza.Web.Api;

public static class Startup
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        return services
            .AddMappers()
            .AddApiRepositories()
            .AddTransient<IUrl, Url>()
            .AddTransient<IApiHttpHelper, ApiHttpHelper>()
            .AddTransient<IStartupApi, StartupApi>();
    }

    private static IServiceCollection AddMappers(this IServiceCollection services)
    {
        return services
            .AddTransient<IViewModelMapper, ViewModelMapper>()
            .AddTransient<IDataTransferObjectMapper, DataTransferObjectMapper>();
    }

    private static IServiceCollection AddApiRepositories(this IServiceCollection services)
    {
        return services
            .AddTransient<IArtworkApi, ArtworkApi>()
            .AddTransient<IFavouritesApi, FavouritesApi>()
            .AddTransient<IHistoryApi, HistoryApi>()
            .AddTransient<ILastFmApi, LastFmApi>()
            .AddTransient<ILibraryApi, LibraryApi>()
            .AddTransient<IPlayApi, PlayApi>()
            .AddTransient<ISearchApi, SearchApi>()
            .AddTransient<IStartupApi, StartupApi>()
            .AddTransient<IUpdateApi, UpdateApi>();
    }
}
