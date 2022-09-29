using Cadenza.Web.Common.Interfaces;

namespace Cadenza._Startup;

public static class Dependencies
{
    public static WebAssemblyHostBuilder RegisterDependencies(this WebAssemblyHostBuilder builder)
    {
        var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };

        builder.Services
            .AddCoreServices()
            .AddInteropServices()
            .AddUtilities()
            .AddHttpHelper(sp => http)
            .AddComponents()
            .AddLocalSource<HtmlPlayer>(builder.Configuration, "LocalApi")
            .AddLastFm(builder.Configuration, "LastFmApi")
            .AddDatabase(builder.Configuration, "DatabaseApi");

        return builder;
    }

    private static IServiceCollection AddInteropServices(this IServiceCollection services)
    {
        return services
            .AddTransient<INavigation, NavigationInterop>()
            .AddTransient<IStore, StoreInterop>();
    }
}