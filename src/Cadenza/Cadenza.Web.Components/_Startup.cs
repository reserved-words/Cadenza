using Cadenza.Web.Components.Players;
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

    public static IServiceCollection AddPlayerComponent(this IServiceCollection services)
    {
        return services
            .AddScoped<IPlayTimer, PlayTimer>()
            .AddScoped<IPlayer, CorePlayer>();
    }
}
