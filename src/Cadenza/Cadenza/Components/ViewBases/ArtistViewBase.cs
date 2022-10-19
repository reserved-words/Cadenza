namespace Cadenza.Components.ViewBases;

public class ArtistViewBase : ComponentBase, IDisposable
{

    [Inject]
    public IMessenger Messenger { get; set; }

    [Parameter]
    public ArtistInfo Model { get; set; } = new();

    private Guid _updateSubscriptionId = Guid.Empty;

    public void Dispose()
    {
        if (_updateSubscriptionId != Guid.Empty)
        {
            Messenger.Unsubscribe<ArtistUpdatedEventArgs>(_updateSubscriptionId);
            _updateSubscriptionId = Guid.Empty;
        }
    }

    protected override void OnInitialized()
    {
        Messenger.Subscribe<ArtistUpdatedEventArgs>(OnArtistUpdated, out _updateSubscriptionId);
    }

    private Task OnArtistUpdated(object sender, ArtistUpdatedEventArgs args)
    {
        if (args.Update.Id == Model.Id)
        {
            args.Update.ApplyUpdates(Model);
            StateHasChanged();
        }

        return Task.CompletedTask;
    }
}
