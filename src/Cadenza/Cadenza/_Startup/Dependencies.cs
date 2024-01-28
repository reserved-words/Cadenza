using Cadenza.Common.Http;
using Cadenza.Web.Common.Enums;
using Cadenza.Web.Api;

namespace Cadenza._Startup;

public static class Dependencies
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .RegisterExternalHttpHelper()
            .RegisterApiHttpClient(configuration, HttpClientName.Local, "LocalApi:BaseUrl")
            .RegisterApiHttpClient(configuration, HttpClientName.Database, "Api:BaseUrl");

        return services
            .AddApi()
            .AddPlayer()
            .AddCoreServices()
            .AddInteropServices()
            .AddUtilities()
            .AddComponents()
            .AddLocalSource<HtmlPlayer>();
    }

    private static IServiceCollection RegisterExternalHttpHelper(this IServiceCollection services)
    {
        services.AddHttpClient(HttpClientDefault.Name);

        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
          .CreateClient(HttpClientDefault.Name));

        services.AddHttpHelper();

        return services;
    }

    private static IServiceCollection RegisterApiHttpClient(this IServiceCollection services, IConfiguration configuration, HttpClientName clientName, string configBaseUrl)
    {
        services
            .AddHttpClient(clientName.ToString(), client => client.BaseAddress = new Uri(configuration[configBaseUrl]));

        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
          .CreateClient(clientName.ToString()));

        return services;
    }

    private static IServiceCollection AddInteropServices(this IServiceCollection services)
    {
        return services
            .AddTransient<INavigation, NavigationInterop>();
    }
}
