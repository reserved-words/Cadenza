using Cadenza.Web.Common.Interfaces.Store;
using Cadenza.Web.Info;
using Cadenza.Web.Player;

namespace Cadenza._Startup;

public static class Dependencies
{
    public static WebAssemblyHostBuilder RegisterDependencies(this WebAssemblyHostBuilder builder)
    {
        var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

        builder.Services
            .AddPlayerComponent()
            .AddCoreServices()
            .AddInteropServices()
            .AddUtilities()
            .AddHttpHelper(sp => http)
            .AddComponents()
            .AddLocalSource<HtmlPlayer>()
            .AddLastFm()
            .AddWebInfo()
            .AddDatabase();

        return builder;
    }

    private static IServiceCollection AddInteropServices(this IServiceCollection services)
    {
        return services
            .AddTransient<INavigation, NavigationInterop>()
            .AddTransient<IStore, StoreInterop>();
    }
}