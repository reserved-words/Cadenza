namespace Cadenza.Components.ViewBases;

public class AlbumViewBase : ComponentBase, IDisposable
{
    [Parameter]
    public AlbumInfo Model { get; set; } = new();

    [Inject]
    public IMessenger Messenger { get; set; }

    private Guid _updateSubscriptionId = Guid.Empty;

    public void Dispose()
    {
        if (_updateSubscriptionId != Guid.Empty)
        {
            Messenger.Unsubscribe<AlbumUpdatedEventArgs>(_updateSubscriptionId);
            _updateSubscriptionId = Guid.Empty;
        }
    }

    protected override void OnInitialized()
    {
        Messenger.Subscribe<AlbumUpdatedEventArgs>(OnAlbumUpdated, out _updateSubscriptionId);
    }

    private Task OnAlbumUpdated(object sender, AlbumUpdatedEventArgs args)
    {
        if (Model != null && Model.Id.ToString() == args.Update.Id)
        {
            args.Update.ApplyUpdates(Model);
            StateHasChanged();
        }

        return Task.CompletedTask;
    }
}
