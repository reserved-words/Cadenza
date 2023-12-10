namespace Cadenza.Web.Components.MudServices;

internal class MudNotificationService : INotificationService
{
    private readonly ISnackbar _snackbar;

    public MudNotificationService(ISnackbar snackbar)
    {
        _snackbar = snackbar;
    }

    public void Error(string message)
    {
        _snackbar.Add(message, Severity.Error, options => options.RequireInteraction = true);
    }

    public void Success(string message)
    {
        _snackbar.Add(message, Severity.Success);
    }

    public void Information(string message)
    {
        _snackbar.Add(message, Severity.Info);
    }
}