namespace Cadenza.Components.ViewBases;

public class LyricsViewBase : ComponentBase, IDisposable
{
    [Parameter]
    public TrackInfo Model { get; set; } = new();

    [Inject]
    public IMessenger Messenger { get; set; }

    public MarkupString Lyrics => (MarkupString)Model.Lyrics.WithLineBreaks();

    private Guid _updateSubscriptionId = Guid.Empty;

    public void Dispose()
    {
        if (_updateSubscriptionId != Guid.Empty)
        {
            Messenger.Unsubscribe<LyricsUpdatedEventArgs>(_updateSubscriptionId);
            _updateSubscriptionId = Guid.Empty;
        }
    }

    protected override void OnInitialized()
    {
        Messenger.Subscribe<LyricsUpdatedEventArgs>(OnLyricsUpdated, out _updateSubscriptionId);
    }

    private Task OnLyricsUpdated(object sender, LyricsUpdatedEventArgs args)
    {
        if (Model != null && args.Update.Id == Model.Id)
        {
            args.Update.ApplyUpdates(Model);
            StateHasChanged();
        }

        return Task.CompletedTask;
    }
}
