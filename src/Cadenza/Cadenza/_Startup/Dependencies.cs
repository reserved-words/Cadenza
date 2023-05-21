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
            .RegisterApiHttpClient<LocalApiAuthorizationMessageHandler>(configuration, "LocalAPI", "LocalApi:BaseUrl")
            .RegisterApiHttpClient<MainApiAuthorizationMessageHandler>(configuration, "MainAPI", "DatabaseApi:BaseUrl");

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
        services.AddHttpClient("External");

        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
          .CreateClient("External"));

        services.AddDefaultHttpHelper();

        return services;
    }

    private static IServiceCollection RegisterApiHttpClient<THandler>(this IServiceCollection services, IConfiguration configuration, string apiName, string configBaseUrl) 
        where THandler : AuthorizationMessageHandler
    {
        services
            .AddTransient<THandler>()
            .AddHttpClient(apiName, client => client.BaseAddress = new Uri(configuration[configBaseUrl]))
            .AddHttpMessageHandler<THandler>();

        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
          .CreateClient(apiName));

        return services;
    }

    private static IServiceCollection AddInteropServices(this IServiceCollection services)
    {
        return services
            .AddTransient<IDebugLogger, ConsoleLogger>()
            .AddTransient<INavigation, NavigationInterop>()
            .AddTransient<IStore, StoreInterop>();
    }
}

public class MainApiAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public MainApiAuthorizationMessageHandler(IAccessTokenProvider provider,
        NavigationManager navigation, IConfiguration config)
        : base(provider, navigation)
    {
        var baseUrl = config["DatabaseApi:BaseUrl"];
        var scope = config["Authentication:Scopes:Database"];

        ConfigureHandler(
            authorizedUrls: new[] { baseUrl },
            scopes: new List<string> { scope });
    }
}

public class LocalApiAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public LocalApiAuthorizationMessageHandler(IAccessTokenProvider provider,
        NavigationManager navigation, IConfiguration config)
        : base(provider, navigation)
    {
        var baseUrl = config["LocalApi:BaseUrl"];
        var scope = config["Authentication:Scopes:Local"];

        ConfigureHandler(
            authorizedUrls: new[] { baseUrl },
            scopes: new List<string> { scope } );
    }
}