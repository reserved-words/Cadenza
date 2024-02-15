global using Cadenza.Common.Enums;
global using Cadenza.Web.Common.Enums;
global using Cadenza.Web.Common.Extensions;
global using Cadenza.Web.Common.Interfaces;
global using Cadenza.Web.Common.Model;
global using Cadenza.Web.Common.ViewModel;
global using Cadenza.Web.Components.MudServices;
global using Cadenza.Web.State.Actions;
global using Cadenza.Web.State.Store;
global using Fluxor;
global using Fluxor.Blazor.Web.Components;
global using Microsoft.AspNetCore.Components;
global using Microsoft.Extensions.DependencyInjection;
global using MudBlazor;
global using MudBlazor.Services;
global using System.Web;

using IDialogService = Cadenza.Web.Components.Interfaces.IDialogService;

namespace Cadenza.Web.Components;

public static class Startup
{
    public static IServiceCollection AddComponents(this IServiceCollection services)
    {
        return services.AddUIHelpers();
    }

    private static IServiceCollection AddUIHelpers(this IServiceCollection services)
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
            .AddTransient<IDialogService, MudDialogService>()
            .AddTransient<IStartupDialogService, StartupDialogService>()
            .AddTransient<INotificationService, MudNotificationService>();
    }
}
