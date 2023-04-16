namespace Cadenza.Components.ViewBases;

public class TrackViewBase : ComponentBase, IDisposable
{
    [Parameter]
    public TrackInfo Model { get; set; } = new();

    [Inject]
    public IMessenger Messenger { get; set; }

    private Guid _updateSubscriptionId = Guid.Empty;

    public void Dispose()
    {
        if (_updateSubscriptionId != Guid.Empty)
        {
            Messenger.Unsubscribe<TrackUpdatedEventArgs>(_updateSubscriptionId);
            _updateSubscriptionId = Guid.Empty;
        }
    }

    protected override void OnInitialized()
    {
        Messenger.Subscribe<TrackUpdatedEventArgs>(OnTrackUpdated, out _updateSubscriptionId);
    }

    private Task OnTrackUpdated(object sender, TrackUpdatedEventArgs args)
    {
        if (args.Update.Id == Model.Id.ToString())
        {
            args.Update.ApplyUpdates(Model);
            StateHasChanged();
        }

        return Task.CompletedTask;
    }
}
