using Cadenza.Common.Http;
using Cadenza.HttpMessageHandlers;
using Cadenza.Web.Common.Enums;
using Cadenza.Web.Info;
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
            .AddPlayer()
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
        services.AddHttpClient(HttpClientDefault.Name);

        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
          .CreateClient(HttpClientDefault.Name));

        services.AddHttpHelper();

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
