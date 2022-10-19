namespace Cadenza.Components.ViewBases;

public class ArtistViewBase : ComponentBase, IDisposable
{

    [Inject]
    public IMessenger Messenger { get; set; }

    [Inject]
    public IWebInfoService WebInfoService { get; set; }

    [Parameter]
    public ArtistInfo Model { get; set; } = new();

    private Guid _updateSubscriptionId = Guid.Empty;

    protected string ImageUrl { get; set; }

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

    protected override async Task OnParametersSetAsync()
    {
        // TODO: Error handling
        ImageUrl = await WebInfoService.GetArtistImageUrl(Model);
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
