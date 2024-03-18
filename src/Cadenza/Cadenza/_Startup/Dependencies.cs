using Cadenza.Common.Http;
using Cadenza.Web.Common.Enums;
using Cadenza.Web.Api;
using MudBlazor.Services;
using MudBlazor;
using Cadenza.Services;
using Cadenza.Interfaces;

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

    private static IServiceCollection AddComponents(this IServiceCollection services)
    {
        return services
            .AddMudServices(config =>
            {
                config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;
                config.SnackbarConfiguration.PreventDuplicates = false;
                config.SnackbarConfiguration.NewestOnTop = false;
                config.SnackbarConfiguration.ShowCloseIcon = true;
                config.SnackbarConfiguration.VisibleStateDuration = 10000;
                config.SnackbarConfiguration.HideTransitionDuration = 500;
                config.SnackbarConfiguration.ShowTransitionDuration = 500;
                config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            })
            .AddTransient<Interfaces.IDialogService, MudDialogService>()
            .AddTransient<IStartupDialogService, MudStartupDialogService>()
            .AddTransient<INotificationService, MudNotificationService>();
    }
}
