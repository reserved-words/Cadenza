namespace Cadenza.Components.Sidebar;

public class ToolbarSourceBase : ComponentBase
{
    [Inject]
    public INotificationService Notification { get; set; }

    [Inject]
    public IConnectorConsumer ConnectorService { get; set; }

    [Parameter]
    public ConnectorStatusViewModel Model { get; set; }

    protected override void OnInitialized()
    {
        ConnectorService.ConnectorStatusChanged += OnConnectorStatusChanged;
    }

    private Task OnConnectorStatusChanged(object sender, ConnectorEventArgs e)
    {
        if (e.Connector != Model.Connector)
            return Task.CompletedTask;

        Model.Status = e.Status;

        Model.ErrorTitle = e.Status == ConnectorStatus.Errored
            ? $"{e.Connector} errored"
            : null;

        Model.ErrorMessage = e.Status == ConnectorStatus.Errored
            ? e.Error
            : null;

        Model.ShowError = false;

        //if (e.Status == ConnectorStatus.Errored)
        //{
        //    Notification.Error($"{e.Connector} error: {e.Error}");
        //}

        StateHasChanged();

        return Task.CompletedTask;
    }

    protected Color Color => Model.Status == ConnectorStatus.Errored
        ? Color.Error
        : Model.Status == ConnectorStatus.Disabled
        ? Color.Default
        : Model.Status == ConnectorStatus.Connected
        ? Color.Success
        : Color.Warning;


    protected void HideError()
    {
        Model.ShowError = false;
        StateHasChanged();
    }

    protected void ShowError()
    {
        Model.ShowError = true;
        StateHasChanged();
    }
}