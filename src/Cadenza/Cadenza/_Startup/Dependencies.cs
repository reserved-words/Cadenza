using Cadenza.HttpMessageHandlers;
using Cadenza.Web.Common.Interfaces.Store;
using Cadenza.Web.Info;
using Cadenza.Web.Player;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Cadenza._Startup;

public static class Dependencies
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .RegisterExternalHttpHelper()
            .RegisterApiHttpClient<LocalApiAuthorizationMessageHandler>(configuration, HttpClientName.Local, "LocalApi:BaseUrl")
            .RegisterApiHttpClient<MainApiAuthorizationMessageHandler>(configuration, HttpClientName.Database, "DatabaseApi:BaseUrl");

        return services
            .AddDatabase()
            .AddPlayerComponent()
            .AddCoreServices()
            .AddInteropServices()
            .AddUtilities()
            .AddComponents()
            .AddLocalSource<HtmlPlayer>()
            .AddLastFm()
            .AddWebInfo();
    }

    private static IServiceCollection RegisterExternalHttpHelper(this IServiceCollection services)
    {
        services.AddHttpClient(HttpClientName.Default.ToString());

        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
          .CreateClient(HttpClientName.Default.ToString()));

        services.AddDefaultHttpHelper();

        return services;
    }

    private static IServiceCollection RegisterApiHttpClient<THandler>(this IServiceCollection services, IConfiguration configuration, HttpClientName clientName, string configBaseUrl) 
        where THandler : AuthorizationMessageHandler
    {
        services
            .AddTransient<THandler>()
            .AddHttpClient(clientName.ToString(), client => client.BaseAddress = new Uri(configuration[configBaseUrl]))
            .AddHttpMessageHandler<THandler>();

        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
          .CreateClient(clientName.ToString()));

        return services;
    }

    private static IServiceCollection AddInteropServices(this IServiceCollection services)
    {
        return services
            .AddTransient<INavigation, NavigationInterop>()
            .AddTransient<IStore, StoreInterop>();
    }
}
